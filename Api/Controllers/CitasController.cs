using Api.Security;
using Application.DTOs.Cita;
using Application.UseCases.Citas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class CitasController : ControllerBase
{
    private readonly GetAllCitaUseCase _getAll;
    private readonly GetCitaByIdUseCase _getById;
    private readonly CreateCitaUseCase _create;
    private readonly UpdateCitaUseCase _update;
    private readonly CancelCitaUseCase _cancel;
    private readonly ReprogramarCitaUseCase _reprogramar;
    private readonly DeleteCitaUseCase _delete;

    public CitasController(
        GetAllCitaUseCase getAll,
        GetCitaByIdUseCase getById,
        CreateCitaUseCase create,
        UpdateCitaUseCase update,
        CancelCitaUseCase cancel,
        ReprogramarCitaUseCase reprogramar,
        DeleteCitaUseCase delete)
        => (_getAll, _getById, _create, _update, _cancel, _reprogramar, _delete)
            = (getAll, getById, create, update, cancel, reprogramar, delete);

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
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCitaDto request)
    {
        try
        {
            await _update.ExecuteAsync(id, request);
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
            await _cancel.ExecuteAsync(id, request);
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
            await _reprogramar.ExecuteAsync(id, request);
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
            await _delete.ExecuteAsync(id, motivoCancelacion);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
