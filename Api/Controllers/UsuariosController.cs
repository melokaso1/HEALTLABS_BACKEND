using Api.Security;
using Application.DTOs.Usuario;
using Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    private readonly DeleteUsuarioUseCase _delete;

    public UsuariosController(GetAllUsuarioUseCase getAll, GetUsuarioByIdUseCase getById,
        CreateUsuarioUseCase create, UpdateUsuarioUseCase update, DeleteUsuarioUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var usuario = await _getById.ExecuteAsync(id);
        return usuario is null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto request)
    {
        try
        {
            var usuario = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, request);
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
