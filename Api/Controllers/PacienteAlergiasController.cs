using Api.Security;
using Application.DTOs.PacienteAlergia;
using Application.UseCases.PacienteAlergia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class PacienteAlergiasController : ControllerBase
{
    private readonly GetAllPacienteAlergiaUseCase _getAll;
    private readonly GetPacienteAlergiaByIdUseCase _getById;
    private readonly CreatePacienteAlergiaUseCase _create;
    private readonly UpdatePacienteAlergiaUseCase _update;
    private readonly DeletePacienteAlergiaUseCase _delete;

    public PacienteAlergiasController(GetAllPacienteAlergiaUseCase getAll, GetPacienteAlergiaByIdUseCase getById,
        CreatePacienteAlergiaUseCase create, UpdatePacienteAlergiaUseCase update, DeletePacienteAlergiaUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("paciente/{pacienteId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByPacienteId(Guid pacienteId) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.PacienteId == pacienteId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Create([FromBody] CreatePacienteAlergiaDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePacienteAlergiaDto request)
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
