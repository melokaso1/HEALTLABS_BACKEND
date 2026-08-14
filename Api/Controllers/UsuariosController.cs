using Api.Security;
using Application.DTOs.Usuario;
using Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class UsuariosController : ControllerBase
{
    private readonly GetAllUsuarioUseCase _getAll;
    private readonly GetUsuarioByIdUseCase _getById;
    private readonly CreateUsuarioUseCase _create;
    private readonly UpdateUsuarioUseCase _update;
    private readonly SetUsuarioActivoUseCase _setActivo;
    private readonly DeleteUsuarioUseCase _delete;

    public UsuariosController(
        GetAllUsuarioUseCase getAll,
        GetUsuarioByIdUseCase getById,
        CreateUsuarioUseCase create,
        UpdateUsuarioUseCase update,
        SetUsuarioActivoUseCase setActivo,
        DeleteUsuarioUseCase delete)
        => (_getAll, _getById, _create, _update, _setActivo, _delete)
            = (getAll, getById, create, update, setActivo, delete);

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await _getById.ExecuteAsync(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto request)
    {
        try
        {
            var usuario = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, usuario);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto request)
    {
        try
        {
            await _update.ExecuteAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        catch (DbUpdateException ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpPut("{id:guid}/activo")]
    public async Task<IActionResult> SetActivo(Guid id, [FromBody] SetUsuarioActivoDto request)
    {
        try
        {
            await _setActivo.ExecuteAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        catch (DbUpdateException ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _getById.ExecuteAsync(id);
            await _delete.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
