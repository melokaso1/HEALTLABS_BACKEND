using Api.Security;
using Application.DTOs.Cita;
using Application.UseCases.Cita;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class CitasController : ControllerBase
{
    private readonly CitaCrudUseCase _useCase;

    public CitasController(CitaCrudUseCase useCase) => _useCase = useCase;

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _useCase.GetAllAsync());

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _useCase.GetByIdAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        try
        {
            var entity = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, request);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCitaDto request)
    {
        try
        {
            await _useCase.UpdateAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/cancelar")]
    public async Task<IActionResult> Cancelar(Guid id, [FromBody] CancelCitaDto request)
    {
        try
        {
            await _useCase.CancelAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:guid}/reprogramar")]
    public async Task<IActionResult> Reprogramar(Guid id, [FromBody] ReprogramarCitaDto request)
    {
        try
        {
            await _useCase.ReprogramarAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] string motivoCancelacion = "Cancelada por usuario")
    {
        try
        {
            await _useCase.CancelAsync(id, new CancelCitaDto { MotivoCancelacion = motivoCancelacion });
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
