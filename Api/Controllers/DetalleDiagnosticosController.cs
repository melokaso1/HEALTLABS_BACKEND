using Api.Security;
using Application.DTOs.DetalleDiagnostico;
using Application.UseCases.DetalleDiagnostico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class DetalleDiagnosticosController : ControllerBase
{
    private readonly GetAllDetalleDiagnosticoUseCase _getAll;
    private readonly GetDetalleDiagnosticoByIdUseCase _getById;
    private readonly CreateDetalleDiagnosticoUseCase _create;
    private readonly DeleteDetalleDiagnosticoUseCase _delete;

    public DetalleDiagnosticosController(GetAllDetalleDiagnosticoUseCase getAll, GetDetalleDiagnosticoByIdUseCase getById,
        CreateDetalleDiagnosticoUseCase create, DeleteDetalleDiagnosticoUseCase delete)
        => (_getAll, _getById, _create, _delete) = (getAll, getById, create, delete);

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
    [Authorize(Roles = AppRoles.Medico)]
    public async Task<IActionResult> Create([FromBody] CreateDetalleDiagnosticoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Medico)]
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
