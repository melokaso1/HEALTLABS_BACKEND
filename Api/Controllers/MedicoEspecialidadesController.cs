using Api.Security;
using Application.DTOs.MedicoEspecialidad;
using Application.UseCases.MedicoEspecialidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class MedicoEspecialidadesController : ControllerBase
{
    private readonly GetAllMedicoEspecialidadUseCase _getAll;
    private readonly GetMedicoEspecialidadByIdUseCase _getById;
    private readonly CreateMedicoEspecialidadUseCase _create;
    private readonly DeleteMedicoEspecialidadUseCase _delete;

    public MedicoEspecialidadesController(GetAllMedicoEspecialidadUseCase getAll, GetMedicoEspecialidadByIdUseCase getById,
        CreateMedicoEspecialidadUseCase create, DeleteMedicoEspecialidadUseCase delete)
        => (_getAll, _getById, _create, _delete) = (getAll, getById, create, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("medico/{medicoId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByMedicoId(Guid medicoId) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.MedicoId == medicoId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateMedicoEspecialidadDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
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
