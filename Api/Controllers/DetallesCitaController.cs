using Api.Security;
using Application.DTOs.DetalleCita;
using Application.UseCases.DetalleCita;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class DetallesCitaController : ControllerBase
{
    private readonly GetAllDetalleCitaUseCase _getAll;
    private readonly GetDetalleCitaByIdUseCase _getById;
    private readonly CreateDetalleCitaUseCase _create;
    private readonly UpdateDetalleCitaUseCase _update;
    private readonly DeleteDetalleCitaUseCase _delete;

    public DetallesCitaController(GetAllDetalleCitaUseCase getAll, GetDetalleCitaByIdUseCase getById,
        CreateDetalleCitaUseCase create, UpdateDetalleCitaUseCase update, DeleteDetalleCitaUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet("cita/{idCita:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByCitaId(Guid idCita) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.CitaId == idCita));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDetalleCitaDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDetalleCitaDto request)
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
