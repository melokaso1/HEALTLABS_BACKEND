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
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Empleado)
            .Include(u => u.Rol)
            .Select(u => new UsuarioDto
            {
                UsuarioId = u.Id,
                EmpleadoId = u.EmpleadoId,
                RolId = u.RolId,
                Username = u.Username,
                Email = u.Email,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion,
                UltimoLogin = u.UltimoLogin,
                DebeCambiarPassword = u.DebeCambiarPassword,
                TokenVersion = u.TokenVersion
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var usuario = await _context.Usuarios
            .Where(u => u.Id == id)
            .Select(u => new UsuarioDto
            {
                UsuarioId = u.Id,
                EmpleadoId = u.EmpleadoId,
                RolId = u.RolId,
                Username = u.Username,
                Email = u.Email,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion,
                UltimoLogin = u.UltimoLogin,
                DebeCambiarPassword = u.DebeCambiarPassword,
                TokenVersion = u.TokenVersion
            })
            .FirstOrDefaultAsync();

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto request)
    {
        var empleadoExiste = await _context.Empleados.AnyAsync(e => e.Id == request.EmpleadoId);
        if (!empleadoExiste)
            return BadRequest("El empleado especificado no existe.");

        var rolExiste = await _context.Roles.AnyAsync(r => r.Id == request.RolId);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe.");

        var usernameExiste = await _context.Usuarios.AnyAsync(u => u.Username == request.Username);
        if (usernameExiste)
            return BadRequest("El nombre de usuario ya se encuentra registrado.");

        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);
        if (emailExiste)
            return BadRequest("El correo electrónico ya se encuentra registrado.");

        var nuevoUsuario = new UsuarioEntity(
            empleadoId: request.EmpleadoId,
            rolId: request.RolId,
            username: request.Username,
            email: request.Email,
            passwordHash: request.Password, 
            activo: request.Activo,
            ultimoLogin: null,
            intentosFallidos: 0,
            bloqueadoHasta: null,
            debeCambiarPassword: request.DebeCambiarPassword,
            passwordChangedAt: null,
            tokenVersion: 1
        );

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        var usuarioDto = new UsuarioDto
        {
            UsuarioId = nuevoUsuario.Id,
            EmpleadoId = nuevoUsuario.EmpleadoId,
            RolId = nuevoUsuario.RolId,
            Username = nuevoUsuario.Username,
            Email = nuevoUsuario.Email,
            Activo = nuevoUsuario.Activo,
            FechaCreacion = nuevoUsuario.FechaCreacion,
            UltimoLogin = nuevoUsuario.UltimoLogin,
            DebeCambiarPassword = nuevoUsuario.DebeCambiarPassword,
            TokenVersion = nuevoUsuario.TokenVersion
        };

        return CreatedAtAction(nameof(GetById), new { id = usuarioDto.UsuarioId }, usuarioDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
            return NotFound();

        var empleadoExiste = await _context.Empleados.AnyAsync(e => e.Id == request.EmpleadoId);
        if (!empleadoExiste)
            return BadRequest("El empleado especificado no existe.");

        var rolExiste = await _context.Roles.AnyAsync(r => r.Id == request.RolId);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe.");

        var usernameExiste = await _context.Usuarios.AnyAsync(u => u.Username == request.Username && u.Id != id);
        if (usernameExiste)
            return BadRequest("El nombre de usuario ya se encuentra en uso por otro usuario.");

        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == request.Email && u.Id != id);
        if (emailExiste)
            return BadRequest("El correo electrónico ya se encuentra en uso por otro usuario.");

        var passwordHash = string.IsNullOrWhiteSpace(request.Password)
            ? usuario.PasswordHash
            : request.Password;

        var passwordChangedAt = string.IsNullOrWhiteSpace(request.Password)
            ? usuario.PasswordChangedAt
            : DateTime.UtcNow;

        var tokenVersion = (!string.IsNullOrWhiteSpace(request.Password) || usuario.Activo != request.Activo)
            ? usuario.TokenVersion + 1
            : usuario.TokenVersion;

        usuario.Update(
            empleadoId: request.EmpleadoId,
            rolId: request.RolId,
            username: request.Username,
            email: request.Email,
            passwordHash: passwordHash,
            activo: request.Activo,
            ultimoLogin: usuario.UltimoLogin,
            intentosFallidos: usuario.IntentosFallidos,
            bloqueadoHasta: usuario.BloqueadoHasta,
            debeCambiarPassword: request.DebeCambiarPassword,
            passwordChangedAt: passwordChangedAt,
            tokenVersion: tokenVersion
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
            return NotFound();

        usuario.Update(
            empleadoId: usuario.EmpleadoId,
            rolId: usuario.RolId,
            username: usuario.Username,
            email: usuario.Email,
            passwordHash: usuario.PasswordHash,
            activo: false, 
            ultimoLogin: usuario.UltimoLogin,
            intentosFallidos: usuario.IntentosFallidos,
            bloqueadoHasta: usuario.BloqueadoHasta,
            debeCambiarPassword: usuario.DebeCambiarPassword,
            passwordChangedAt: usuario.PasswordChangedAt,
            tokenVersion: usuario.TokenVersion + 1 
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}