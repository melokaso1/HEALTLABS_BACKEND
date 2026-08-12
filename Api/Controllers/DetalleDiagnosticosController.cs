using Api.Security;
using Application.DTOs.DetalleDiagnostico;
using Application.UseCases.DetalleDiagnostico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class DetalleDiagnosticosController : ControllerBase
{
    private readonly GetAllDetalleDiagnosticoUseCase _getAll;
    private readonly GetDetalleDiagnosticoByIdUseCase _getById;
    private readonly CreateDetalleDiagnosticoUseCase _create;
    private readonly UpdateDetalleDiagnosticoUseCase _update;
    private readonly DeleteDetalleDiagnosticoUseCase _delete;

    public DetalleDiagnosticosController(GetAllDetalleDiagnosticoUseCase getAll, GetDetalleDiagnosticoByIdUseCase getById,
        CreateDetalleDiagnosticoUseCase create, UpdateDetalleDiagnosticoUseCase update, DeleteDetalleDiagnosticoUseCase delete)
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
    public async Task<IActionResult> Create([FromBody] CreateDetalleDiagnosticoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDetalleDiagnosticoDto request)
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
