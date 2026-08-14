using Api.Security;
using Application.DTOs.Medico;
using Application.Exceptions;
using Application.UseCases.Medicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class MedicosController : ControllerBase
{
    private readonly GetAllMedicoUseCase _getAll;
    private readonly GetMedicoByIdUseCase _getById;
    private readonly CreateMedicoUseCase _create;
    private readonly CreateMedicoCompletoUseCase _createCompleto;
    private readonly UpdateMedicoUseCase _update;
    private readonly DeleteMedicoUseCase _delete;

    public MedicosController(GetAllMedicoUseCase getAll, GetMedicoByIdUseCase getById,
        CreateMedicoUseCase create, CreateMedicoCompletoUseCase createCompleto,
        UpdateMedicoUseCase update, DeleteMedicoUseCase delete)
        => (_getAll, _getById, _create, _createCompleto, _update, _delete)
            = (getAll, getById, create, createCompleto, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateMedicoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("completo")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> CreateCompleto([FromBody] CreateMedicoCompletoDto request)
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
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicoDto request)
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
            await _delete.ExecuteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
