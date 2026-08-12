using Api.Security;
using Application.DTOs.AtencionTratamiento;
using Application.UseCases.AtencionTratamiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class AtencionesTratamientoController : ControllerBase
{
    private readonly GetAllAtencionTratamientoUseCase _getAll;
    private readonly GetAtencionTratamientoByIdUseCase _getById;
    private readonly CreateAtencionTratamientoUseCase _create;
    private readonly UpdateAtencionTratamientoUseCase _update;
    private readonly DeleteAtencionTratamientoUseCase _delete;

    public AtencionesTratamientoController(GetAllAtencionTratamientoUseCase getAll, GetAtencionTratamientoByIdUseCase getById,
        CreateAtencionTratamientoUseCase create, UpdateAtencionTratamientoUseCase update, DeleteAtencionTratamientoUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("detalle-cita/{detalleCitaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByDetalleCitaId(Guid detalleCitaId) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.DetalleCitaId == detalleCitaId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAtencionTratamientoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAtencionTratamientoDto request)
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
