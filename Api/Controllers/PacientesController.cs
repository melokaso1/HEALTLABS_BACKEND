using Api.Security;
using Application.DTOs.Paciente;
using Application.Exceptions;
using Application.UseCases.Pacientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class PacientesController : ControllerBase
{
    private readonly GetAllPacienteUseCase _getAll;
    private readonly GetPacienteByIdUseCase _getById;
    private readonly BuscarPacientePorDocumentoUseCase _buscarPorDocumento;
    private readonly CreatePacienteUseCase _create;
    private readonly CreatePacienteCompletoUseCase _createCompleto;
    private readonly UpdatePacienteUseCase _update;
    private readonly DeletePacienteUseCase _delete;

    public PacientesController(GetAllPacienteUseCase getAll, GetPacienteByIdUseCase getById,
        BuscarPacientePorDocumentoUseCase buscarPorDocumento, CreatePacienteUseCase create,
        CreatePacienteCompletoUseCase createCompleto, UpdatePacienteUseCase update, DeletePacienteUseCase delete)
        => (_getAll, _getById, _buscarPorDocumento, _create, _createCompleto, _update, _delete)
            = (getAll, getById, buscarPorDocumento, create, createCompleto, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("buscar")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Buscar([FromQuery] Guid tipoDocumento, [FromQuery] string numeroDocumento)
    {
        var result = await _buscarPorDocumento.ExecuteAsync(tipoDocumento, numeroDocumento);

        if (result.NoExiste)
            return NotFound();

        if (result.EstaInactivo)
            return Conflict("El paciente se encuentra inactivo.");

        return Ok(result.Paciente);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Create([FromBody] CreatePacienteDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("completo")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> CreateCompleto([FromBody] CreatePacienteCompletoDto request)
    {
        try
        {
            var entity = await _createCompleto.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (DuplicateDocumentException ex) { return Conflict(ex.Message); }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            return Conflict("Ya existe una persona registrada con ese tipo y número de documento.");
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePacienteDto request)
    {
        try
        {
            await _update.ExecuteAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (await _getById.ExecuteAsync(id) is null) return NotFound();
            await _delete.ExecuteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
