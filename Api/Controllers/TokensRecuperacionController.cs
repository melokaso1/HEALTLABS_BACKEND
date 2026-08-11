using Api.Security;
using Application.DTOs.TokenRecuperacion;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class TokensRecuperacionController : ControllerBase
{
    private readonly AppDbContext _context;

    public TokensRecuperacionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tokens = await _context.TokensRecuperacion
            .Include(t => t.Usuario)
            .OrderByDescending(t => t.FechaCreacion)
            .Select(t => new TokenRecuperacionDto
            {
                TokenRecuperacionId = t.Id,
                UsuarioId = t.UsuarioId,
                ExpiresAt = t.ExpiresAt,
                UsadoEn = t.UsadoEn,
                IpSolicitud = t.IpSolicitud,
                FechaCreacion = t.FechaCreacion
            })
            .ToListAsync();

        return Ok(tokens);
    }

    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> GetByUsuario(Guid usuarioId)
    {
        var tokens = await _context.TokensRecuperacion
            .Where(t => t.UsuarioId == usuarioId)
            .OrderByDescending(t => t.FechaCreacion)
            .Select(t => new TokenRecuperacionDto
            {
                TokenRecuperacionId = t.Id,
                UsuarioId = t.UsuarioId,
                ExpiresAt = t.ExpiresAt,
                UsadoEn = t.UsadoEn,
                IpSolicitud = t.IpSolicitud,
                FechaCreacion = t.FechaCreacion
            })
            .ToListAsync();

        return Ok(tokens);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var token = await _context.TokensRecuperacion
            .Where(t => t.Id == id)
            .Select(t => new TokenRecuperacionDto
            {
                TokenRecuperacionId = t.Id,
                UsuarioId = t.UsuarioId,
                ExpiresAt = t.ExpiresAt,
                UsadoEn = t.UsadoEn,
                IpSolicitud = t.IpSolicitud,
                FechaCreacion = t.FechaCreacion
            })
            .FirstOrDefaultAsync();

        if (token is null)
            return NotFound();

        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTokenRecuperacionDto request)
    {
        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId);
        if (!usuarioExiste)
            return BadRequest("El usuario especificado no existe.");

        var nuevoToken = new TokenRecuperacionEntity(
            usuarioId: request.UsuarioId,
            tokenHash: request.TokenHash,
            expiresAt: request.ExpiresAt,
            usadoEn: null,
            ipSolicitud: request.IpSolicitud
        );

        _context.TokensRecuperacion.Add(nuevoToken);
        await _context.SaveChangesAsync();

        var dto = new TokenRecuperacionDto
        {
            TokenRecuperacionId = nuevoToken.Id,
            UsuarioId = nuevoToken.UsuarioId,
            ExpiresAt = nuevoToken.ExpiresAt,
            UsadoEn = nuevoToken.UsadoEn,
            IpSolicitud = nuevoToken.IpSolicitud,
            FechaCreacion = nuevoToken.FechaCreacion
        };

        return CreatedAtAction(nameof(GetById), new { id = dto.TokenRecuperacionId }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTokenRecuperacionDto request)
    {
        var token = await _context.TokensRecuperacion.FindAsync(id);
        if (token is null)
            return NotFound();

        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId);
        if (!usuarioExiste)
            return BadRequest("El usuario especificado no existe.");

        token.Update(
            usuarioId: request.UsuarioId,
            tokenHash: request.TokenHash,
            expiresAt: request.ExpiresAt,
            usadoEn: request.UsadoEn,
            ipSolicitud: request.IpSolicitud
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var token = await _context.TokensRecuperacion.FindAsync(id);
        if (token is null)
            return NotFound();

        _context.TokensRecuperacion.Remove(token);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
