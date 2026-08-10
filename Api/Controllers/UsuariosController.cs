using Api.Security;
using Application.DTOs.Usuario;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _context.Usuarios
            .Include(u => p => u.Persona)
            .Include(u => u.Rol)
            .Include(u => u.Cargo)
            .Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Username = u.Username,
                Activo = u.Activo,
                IdPersona = u.IdPersona,
                IdRol = u.IdRol,
                IdCargo = u.IdCargo
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var usuario = await _context.Usuarios
            .Where(u => u.IdUsuario == id)
            .Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Username = u.Username,
                Activo = u.Activo,
                IdPersona = u.IdPersona,
                IdRol = u.IdRol,
                IdCargo = u.IdCargo
            })
            .FirstOrDefaultAsync();

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var rolExiste = await _context.Roles.AnyAsync(r => r.IdRol == request.IdRol);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe.");

        var cargoExiste = await _context.Cargos.AnyAsync(c => c.IdCargo == request.IdCargo);
        if (!cargoExiste)
            return BadRequest("El cargo especificado no existe.");

        var usernameExiste = await _context.Usuarios.AnyAsync(u => u.Username == request.Username);
        if (usernameExiste)
            return BadRequest("El nombre de usuario ya se encuentra registrado.");

        var nuevoUsuario = new Usuario
        {
            IdUsuario = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.Password, 
            Activo = true,
            IdPersona = request.IdPersona,
            IdRol = request.IdRol,
            IdCargo = request.IdCargo
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        var usuarioDto = new UsuarioDto
        {
            IdUsuario = nuevoUsuario.IdUsuario,
            Username = nuevoUsuario.Username,
            Activo = nuevoUsuario.Activo,
            IdPersona = nuevoUsuario.IdPersona,
            IdRol = nuevoUsuario.IdRol,
            IdCargo = nuevoUsuario.IdCargo
        };

        return CreatedAtAction(nameof(GetById), new { id = usuarioDto.IdUsuario }, usuarioDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUsuarioDto request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var rolExiste = await _context.Roles.AnyAsync(r => r.IdRol == request.IdRol);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe.");

        var cargoExiste = await _context.Cargos.AnyAsync(c => c.IdCargo == request.IdCargo);
        if (!cargoExiste)
            return BadRequest("El cargo especificado no existe.");

        usuario.Username = request.Username;
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            usuario.PasswordHash = request.Password; 
        }
        usuario.IdPersona = request.IdPersona;
        usuario.IdRol = request.IdRol;
        usuario.IdCargo = request.IdCargo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
            return NotFound();

        usuario.Activo = false;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}