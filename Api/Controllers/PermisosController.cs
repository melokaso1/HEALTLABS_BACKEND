using Api.Security;
using Application.DTOs.Permiso;
using Application.UseCases.Permiso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class PermisosController : ControllerBase
{
    private readonly GetAllPermisoUseCase _getAll;
    private readonly GetPermisoByIdUseCase _getById;
    private readonly CreatePermisoUseCase _create;
    private readonly UpdatePermisoUseCase _update;
    private readonly DeletePermisoUseCase _delete;

    public PermisosController(GetAllPermisoUseCase getAll, GetPermisoByIdUseCase getById,
        CreatePermisoUseCase create, UpdatePermisoUseCase update, DeletePermisoUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("modulo/{modulo}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByModulo(string modulo) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.Modulo == modulo));

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermisoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePermisoDto request)
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
