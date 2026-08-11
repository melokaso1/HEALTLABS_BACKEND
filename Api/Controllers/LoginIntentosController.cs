using Api.Security;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class LoginIntentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginIntentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var intentos = await _context.LoginIntentos
            .Include(li => li.Usuario)
            .OrderByDescending(li => li.Fecha)
            .ToListAsync();

        return Ok(intentos);
    }

    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> GetByUsuario(Guid usuarioId)
    {
        var intentos = await _context.LoginIntentos
            .Include(li => li.Usuario)
            .Where(li => li.UsuarioId == usuarioId)
            .OrderByDescending(li => li.Fecha)
            .ToListAsync();

        return Ok(intentos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var intento = await _context.LoginIntentos
            .Include(li => li.Usuario)
            .FirstOrDefaultAsync(li => li.Id == id);

        if (intento is null)
            return NotFound();

        return Ok(intento);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] LoginIntentoEntity request)
    {
        var intento = await _context.LoginIntentos.FindAsync(id);
        if (intento is null)
            return NotFound();

        intento.Update(
            usuarioId: request.UsuarioId,
            usernameIntentado: request.UsernameIntentado,
            exitoso: request.Exitoso,
            motivoFallo: request.MotivoFallo,
            ip: request.Ip,
            userAgent: request.UserAgent,
            fecha: request.Fecha
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> DeleteByUsuario(Guid usuarioId)
    {
        var intentos = await _context.LoginIntentos
            .Where(li => li.UsuarioId == usuarioId)
            .ToListAsync();

        if (!intentos.Any())
            return NotFound("No se encontraron registros de intentos para este usuario.");

        _context.LoginIntentos.RemoveRange(intentos);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var intento = await _context.LoginIntentos.FindAsync(id);
        if (intento is null)
            return NotFound();

        _context.LoginIntentos.Remove(intento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}