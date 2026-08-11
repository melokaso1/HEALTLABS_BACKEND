using Api.Security;
using Application.DTOs.CitaHistorialEstado;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class CitaHistorialEstadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CitaHistorialEstadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var historial = await _context.CitasHistorialEstado
            .Include(h => h.Cita)
            .Include(h => h.EstadoAnterior)
            .Include(h => h.EstadoNuevo)
            .Include(h => h.Usuario)
            .Select(h => new CreateCitaHistorialEstadoDto
            {
                CitaId = h.CitaId,
                EstadoAnteriorId = h.EstadoAnteriorId,
                EstadoNuevoId = h.EstadoNuevoId,
                UsuarioId = h.UsuarioId,
                Observacion = h.Observacion
            })
            .ToListAsync();

        return Ok(historial);
    }

    [HttpGet("cita/{citaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByCita(Guid citaId)
    {
        var historial = await _context.CitasHistorialEstado
            .Where(h => h.CitaId == citaId)
            .OrderByDescending(h => h.FechaCambio)
            .Select(h => new CreateCitaHistorialEstadoDto
            {
                CitaId = h.CitaId,
                EstadoAnteriorId = h.EstadoAnteriorId,
                EstadoNuevoId = h.EstadoNuevoId,
                UsuarioId = h.UsuarioId,
                Observacion = h.Observacion
            })
            .ToListAsync();

        return Ok(historial);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var historial = await _context.CitasHistorialEstado
            .Where(h => h.Id == id)
            .Select(h => new CreateCitaHistorialEstadoDto
            {
                CitaId = h.CitaId,
                EstadoAnteriorId = h.EstadoAnteriorId,
                EstadoNuevoId = h.EstadoNuevoId,
                UsuarioId = h.UsuarioId,
                Observacion = h.Observacion
            })
            .FirstOrDefaultAsync();

        if (historial is null)
            return NotFound();

        return Ok(historial);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCitaHistorialEstadoDto request)
    {
        var citaExiste = await _context.Citas.AnyAsync(c => c.Id == request.CitaId);
        if (!citaExiste)
            return BadRequest("La cita especificada no existe.");

        var estadoAntExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoAnteriorId);
        if (!estadoAntExiste)
            return BadRequest("El estado anterior especificado no existe.");

        var estadoNvoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoNuevoId);
        if (!estadoNvoExiste)
            return BadRequest("El nuevo estado especificado no existe.");

        if (request.UsuarioId.HasValue)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId.Value);
            if (!usuarioExiste)
                return BadRequest("El usuario especificado no existe.");
        }

        var nuevoHistorial = new CitaHistorialEstadoEntity(
            citaId: request.CitaId,
            estadoAnteriorId: request.EstadoAnteriorId,
            estadoNuevoId: request.EstadoNuevoId,
            usuarioId: request.UsuarioId,
            observacion: request.Observacion
        );

        _context.CitasHistorialEstado.Add(nuevoHistorial);
        await _context.SaveChangesAsync();

        var historialDto = new CreateCitaHistorialEstadoDto
        {
            CitaId = nuevoHistorial.CitaId,
            EstadoAnteriorId = nuevoHistorial.EstadoAnteriorId,
            EstadoNuevoId = nuevoHistorial.EstadoNuevoId,
            UsuarioId = nuevoHistorial.UsuarioId,
            Observacion = nuevoHistorial.Observacion
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoHistorial.Id }, historialDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateCitaHistorialEstadoDto request)
    {
        var historial = await _context.CitasHistorialEstado.FindAsync(id);
        if (historial is null)
            return NotFound();

        var citaExiste = await _context.Citas.AnyAsync(c => c.Id == request.CitaId);
        if (!citaExiste)
            return BadRequest("La cita especificada no existe.");

        var estadoAntExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoAnteriorId);
        if (!estadoAntExiste)
            return BadRequest("El estado anterior especificado no existe.");

        var estadoNvoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoNuevoId);
        if (!estadoNvoExiste)
            return BadRequest("El nuevo estado especificado no existe.");

        if (request.UsuarioId.HasValue)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId.Value);
            if (!usuarioExiste)
                return BadRequest("El usuario especificado no existe.");
        }

        historial.Update(
            citaId: request.CitaId,
            estadoAnteriorId: request.EstadoAnteriorId,
            estadoNuevoId: request.EstadoNuevoId,
            usuarioId: request.UsuarioId,
            observacion: request.Observacion
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var historial = await _context.CitasHistorialEstado.FindAsync(id);
        if (historial is null)
            return NotFound();

        _context.CitasHistorialEstado.Remove(historial);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}