using Api.Security;
using Application.DTOs.CitaHistorialEstado;
using Application.UseCases.CitaHistorialEstado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CitaHistorialEstadosController : ControllerBase
{
    private readonly GetAllCitaHistorialEstadoUseCase _getAll;
    private readonly GetCitaHistorialEstadoByIdUseCase _getById;
    private readonly CreateCitaHistorialEstadoUseCase _create;
    private readonly DeleteCitaHistorialEstadoUseCase _delete;

    public CitaHistorialEstadosController(GetAllCitaHistorialEstadoUseCase getAll, GetCitaHistorialEstadoByIdUseCase getById,
        CreateCitaHistorialEstadoUseCase create, DeleteCitaHistorialEstadoUseCase delete)
        => (_getAll, _getById, _create, _delete) = (getAll, getById, create, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("cita/{citaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByCitaId(Guid citaId) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.CitaId == citaId));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Staff)]
    public async Task<IActionResult> Create([FromBody] CreateCitaHistorialEstadoDto request)
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
            if (await _getById.ExecuteAsync(id) is null) return NotFound();
            await _delete.ExecuteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
