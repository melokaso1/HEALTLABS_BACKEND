using System.Security.Claims;
using Api.Security;
using Application.DTOs.Cita;
using Application.UseCases.Citas;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CitasController : ControllerBase
{
    private readonly GetAllCitaUseCase _getAll;
    private readonly GetCitaByIdUseCase _getById;
    private readonly CreateCitaUseCase _create;
    private readonly CancelCitaUseCase _cancel;
    private readonly ReprogramarCitaUseCase _reprogramar;
    private readonly MarcarNoAsistioCitaUseCase _marcarNoAsistio;
    private readonly DeleteCitaUseCase _delete;
    private readonly IGenericRepository<UsuarioEntity> _usuarios;
    private readonly IGenericRepository<MedicoEntity> _medicos;
    private readonly Application.Services.IRealtimeNotificationService _realtime;

    public CitasController(
        GetAllCitaUseCase getAll,
        GetCitaByIdUseCase getById,
        CreateCitaUseCase create,
        CancelCitaUseCase cancel,
        ReprogramarCitaUseCase reprogramar,
        MarcarNoAsistioCitaUseCase marcarNoAsistio,
        DeleteCitaUseCase delete,
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<MedicoEntity> medicos,
        Application.Services.IRealtimeNotificationService realtime)
        => (_getAll, _getById, _create, _cancel, _reprogramar, _marcarNoAsistio, _delete, _usuarios, _medicos, _realtime)
            = (getAll, getById, create, cancel, reprogramar, marcarNoAsistio, delete, usuarios, medicos, realtime);

    /// <summary>Lista citas; use pacienteId para historial de paciente. Profesionales solo reciben sus propias citas.</summary>
    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? pacienteId = null,
        [FromQuery] DateOnly? desde = null,
        [FromQuery] DateOnly? hasta = null)
    {
        Guid? medicoId = null;
        if (User.IsInRole(AppRoles.Medico)
            && !User.IsInRole(AppRoles.Admin)
            && !User.IsInRole(AppRoles.Recepcionista))
        {
            medicoId = await ResolveMedicoIdForCurrentUserAsync();
            if (medicoId is null)
                return Ok(Array.Empty<CitaEntity>());
        }

        try
        {
            return Ok(await _getAll.ExecuteAsync(medicoId, pacienteId, desde, hasta));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var entity = await _getById.ExecuteAsync(id);

            if (IsProfessionalOnly())
            {
                var medicoId = await ResolveMedicoIdForCurrentUserAsync();
                if (medicoId is null || entity.MedicoId != medicoId)
                    return NotFound();
            }

            return Ok(entity);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            await _realtime.BroadcastNotificationAsync("Nueva cita agendada", "Se ha programado una nueva cita médica.", "success");
            await _realtime.BroadcastActivityAsync("Sistema de Citas", "agendó una nueva cita", entity.Fecha.ToString("yyyy-MM-dd"), "#00A896");
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.ExclusionViolation })
        {
            return Conflict("El horario seleccionado se solapa con otra cita activa del médico.");
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/cancelar")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Cancelar(Guid id, [FromBody] CancelCitaDto request)
    {
        try
        {
            await _cancel.ExecuteAsync(id, request);
            await _realtime.BroadcastNotificationAsync("Cita cancelada", "Una cita ha sido cancelada.", "warning");
            await _realtime.BroadcastActivityAsync("Usuario", "canceló una cita médica", id.ToString()[..8], "#EC4899");
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/reprogramar")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Reprogramar(Guid id, [FromBody] ReprogramarCitaDto request)
    {
        try
        {
            await _reprogramar.ExecuteAsync(id, request);
            await _realtime.BroadcastNotificationAsync("Cita reprogramada", $"La cita ha sido reprogramada para {request.Fecha}.", "info");
            await _realtime.BroadcastActivityAsync("Usuario", "reprogramó una cita para", request.Fecha.ToString("yyyy-MM-dd"), "#6366F1");
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.ExclusionViolation })
        {
            return Conflict("El horario seleccionado se solapa con otra cita activa del médico.");
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/no-asistio")]
    [Authorize(Roles = $"{AppRoles.Staff},{AppRoles.Medico}")]
    public async Task<IActionResult> MarcarNoAsistio(Guid id, [FromBody] MarcarNoAsistioCitaDto? request)
    {
        try
        {
            await _marcarNoAsistio.ExecuteAsync(id, request ?? new MarcarNoAsistioCitaDto());
            await _realtime.BroadcastNotificationAsync("Inasistencia a cita", "El paciente fue marcado como No Asistió.", "warning");
            await _realtime.BroadcastActivityAsync("Médico/Staff", "marcó inasistencia en cita", id.ToString()[..8], "#EE9B00");
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] string motivoCancelacion = "Cancelada por usuario")
    {
        try
        {
            await _delete.ExecuteAsync(id, motivoCancelacion);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    private async Task<Guid?> ResolveMedicoIdForCurrentUserAsync()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdValue, out var usuarioId))
            return null;

        var usuario = await _usuarios.GetEntityByIdAsync(usuarioId);
        if (usuario is null)
            return null;

        var medico = await _medicos.FirstOrDefaultAsync(m => m.EmpleadoId == usuario.EmpleadoId);
        return medico?.Id;
    }

    private bool IsProfessionalOnly() =>
        User.IsInRole(AppRoles.Medico)
        && !User.IsInRole(AppRoles.Admin)
        && !User.IsInRole(AppRoles.Recepcionista);
}
