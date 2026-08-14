using Api.Security;
using Application.DTOs.Horario;
using Application.UseCases.Horarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class HorariosController : ControllerBase
{
    private readonly GetAllHorarioUseCase _getAll;
    private readonly GetHorarioByIdUseCase _getById;
    private readonly GetHorariosByMedicoIdUseCase _getByMedicoId;
    private readonly CreateHorarioUseCase _create;
    private readonly UpdateHorarioUseCase _update;
    private readonly DeleteHorarioUseCase _delete;

    public HorariosController(
        GetAllHorarioUseCase getAll,
        GetHorarioByIdUseCase getById,
        GetHorariosByMedicoIdUseCase getByMedicoId,
        CreateHorarioUseCase create,
        UpdateHorarioUseCase update,
        DeleteHorarioUseCase delete)
        => (_getAll, _getById, _getByMedicoId, _create, _update, _delete)
            = (getAll, getById, getByMedicoId, create, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("medico/{medicoId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByMedicoId(Guid medicoId) =>
        Ok(await _getByMedicoId.ExecuteAsync(medicoId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateHorarioDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHorarioDto request)
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
