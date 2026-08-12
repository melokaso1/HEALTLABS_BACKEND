using Api.Security;
using Application.DTOs.Antecedente;
using Application.UseCases.Antecedentes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class AntecedentesController : ControllerBase
{
    private readonly GetAllAntecedenteUseCase _getAll;
    private readonly GetAntecedenteByIdUseCase _getById;
    private readonly CreateAntecedenteUseCase _create;
    private readonly UpdateAntecedenteUseCase _update;
    private readonly DeleteAntecedenteUseCase _delete;

    public AntecedentesController(GetAllAntecedenteUseCase getAll, GetAntecedenteByIdUseCase getById,
        CreateAntecedenteUseCase create, UpdateAntecedenteUseCase update, DeleteAntecedenteUseCase delete)
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
    public async Task<IActionResult> Create([FromBody] CreateAntecedenteDto request)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAntecedenteDto request)
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
