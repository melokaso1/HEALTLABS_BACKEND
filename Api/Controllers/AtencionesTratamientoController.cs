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
    private readonly AtencionTratamientoCrudUseCase _useCase;

    public AtencionesTratamientoController(AtencionTratamientoCrudUseCase useCase) => _useCase = useCase;

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _useCase.GetAllAsync());

    [HttpGet("detalle-cita/{detalleCitaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByDetalleCitaId(Guid detalleCitaId) =>
        Ok(await _useCase.FindAsync(entity => entity.DetalleCitaId == detalleCitaId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _useCase.GetByIdAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAtencionTratamientoDto request)
    {
        try
        {
            var entity = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, request);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAtencionTratamientoDto request)
    {
        try
        {
            await _useCase.UpdateAsync(id, request);
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
            await _useCase.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
