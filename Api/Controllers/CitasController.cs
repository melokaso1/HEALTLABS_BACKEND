using System.Security.Claims;
using Api.Security;
using Application.DTOs.Cita;
using Application.UseCases.Citas;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    public CitasController(
        GetAllCitaUseCase getAll,
        GetCitaByIdUseCase getById,
        CreateCitaUseCase create,
        CancelCitaUseCase cancel,
        ReprogramarCitaUseCase reprogramar,
        MarcarNoAsistioCitaUseCase marcarNoAsistio,
        DeleteCitaUseCase delete,
        IGenericRepository<UsuarioEntity> usuarios,
        IGenericRepository<MedicoEntity> medicos)
        => (_getAll, _getById, _create, _cancel, _reprogramar, _marcarNoAsistio, _delete, _usuarios, _medicos)
            = (getAll, getById, create, cancel, reprogramar, marcarNoAsistio, delete, usuarios, medicos);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
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

        return Ok(await _getAll.ExecuteAsync(medicoId));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
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
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/no-asistio")]
    [Authorize(Roles = $"{AppRoles.Staff},{AppRoles.Medico}")]
    public async Task<IActionResult> MarcarNoAsistio(Guid id, [FromBody] MarcarNoAsistioCitaDto? request)
    {
        try
        {
            await _marcarNoAsistio.ExecuteAsync(id, request ?? new MarcarNoAsistioCitaDto());
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
}
