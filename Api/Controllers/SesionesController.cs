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
public sealed class SesionesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SesionesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Sesiones
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sesiones = await _context.Sesiones
            .Include(s => s.Usuario)
            .OrderByDescending(s => s.EmitidoEn)
            .ToListAsync();

        return Ok(sesiones);
    }

    // GET: api/Sesiones/usuario/{usuarioId}
    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> GetByUsuario(Guid usuarioId)
    {
        var sesiones = await _context.Sesiones
            .Include(s => s.Usuario)
            .Where(s => s.UsuarioId == usuarioId)
            .OrderByDescending(s => s.EmitidoEn)
            .ToListAsync();

        return Ok(sesiones);
    }

    // GET: api/Sesiones/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sesion = await _context.Sesiones
            .Include(s => s.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sesion is null)
            return NotFound();

        return Ok(sesion);
    }

    // PUT: api/Sesiones/{id}/revocar
    [HttpPut("{id:guid}/revocar")]
    public async Task<IActionResult> Revocar(Guid id, [FromQuery] string motivo = "Revocado por administrador")
    {
        var sesion = await _context.Sesiones.FindAsync(id);
        if (sesion is null)
            return NotFound();

        // Invocamos el método de negocio de la entidad en lugar de rellenar todo el Update masivo
        sesion.Revocar(motivo);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Sesiones/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sesion = await _context.Sesiones.FindAsync(id);
        if (sesion is null)
            return NotFound();

        _context.Sesiones.Remove(sesion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}