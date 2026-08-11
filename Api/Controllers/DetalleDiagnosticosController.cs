using Api.Security;
using Application.DTOs.DetalleDiagnostico;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class DetalleDiagnosticosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DetalleDiagnosticosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/DetalleDiagnosticos
    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var detalles = await _context.DetallesDiagnostico
            .Include(d => d.DetalleCita)
            .Include(d => d.Diagnostico)
            .Select(d => new CreateDetalleDiagnosticoDto
            {
                DetalleCitaId = d.DetalleCitaId,
                DiagnosticoId = d.DiagnosticoId,
                Principal = d.Principal
            })
            .ToListAsync();

        return Ok(detalles);
    }

    // GET: api/DetallesDiagnostico/detalle-cita/{detalleCitaId}
    [HttpGet("detalle-cita/{detalleCitaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByDetalleCita(Guid detalleCitaId)
    {
        var detalles = await _context.DetallesDiagnostico
            .Include(d => d.Diagnostico)
            .Where(d => d.DetalleCitaId == detalleCitaId)
            .Select(d => new CreateDetalleDiagnosticoDto
            {
                DetalleCitaId = d.DetalleCitaId,
                DiagnosticoId = d.DiagnosticoId,
                Principal = d.Principal
            })
            .ToListAsync();

        return Ok(detalles);
    }

    // GET: api/DetallesDiagnostico/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var detalle = await _context.DetallesDiagnostico
            .Where(d => d.Id == id)
            .Select(d => new CreateDetalleDiagnosticoDto
            {
                DetalleCitaId = d.DetalleCitaId,
                DiagnosticoId = d.DiagnosticoId,
                Principal = d.Principal
            })
            .FirstOrDefaultAsync();

        if (detalle is null)
            return NotFound();

        return Ok(detalle);
    }

    // POST: api/DetallesDiagnostico
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDetalleDiagnosticoDto request)
    {
        var detalleCitaExiste = await _context.DetallesCita.AnyAsync(dc => dc.Id == request.DetalleCitaId);
        if (!detalleCitaExiste)
            return BadRequest("El detalle de cita especificado no existe.");

        var diagnosticoExiste = await _context.Diagnosticos.AnyAsync(d => d.Id == request.DiagnosticoId && d.Activo);
        if (!diagnosticoExiste)
            return BadRequest("El diagnóstico especificado no existe o se encuentra inactivo.");

        var yaAsignado = await _context.DetallesDiagnostico.AnyAsync(d => 
            d.DetalleCitaId == request.DetalleCitaId && 
            d.DiagnosticoId == request.DiagnosticoId);
        if (yaAsignado)
            return BadRequest("Este diagnóstico ya se encuentra asignado a este detalle de cita.");

        // Si se marca como principal, desmarcar los demás diagnósticos del detalle de cita
        if (request.Principal)
        {
            var otrosPrincipales = await _context.DetallesDiagnostico
                .Where(d => d.DetalleCitaId == request.DetalleCitaId && d.Principal)
                .ToListAsync();

            foreach (var item in otrosPrincipales)
            {
                item.Update(item.DetalleCitaId, item.DiagnosticoId, false);
            }
        }

        var nuevoDetalle = new DetalleDiagnosticoEntity(
            detalleCitaId: request.DetalleCitaId,
            diagnosticoId: request.DiagnosticoId,
            principal: request.Principal
        );

        _context.DetallesDiagnostico.Add(nuevoDetalle);
        await _context.SaveChangesAsync();

        var detalleDto = new CreateDetalleDiagnosticoDto
        {
            DetalleCitaId = nuevoDetalle.DetalleCitaId,
            DiagnosticoId = nuevoDetalle.DiagnosticoId,
            Principal = nuevoDetalle.Principal
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoDetalle.Id }, detalleDto);
    }

    // PUT: api/DetallesDiagnostico/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDetalleDiagnosticoDto request)
    {
        var detalle = await _context.DetallesDiagnostico.FindAsync(id);
        if (detalle is null)
            return NotFound();

        var detalleCitaExiste = await _context.DetallesCita.AnyAsync(dc => dc.Id == request.DetalleCitaId);
        if (!detalleCitaExiste)
            return BadRequest("El detalle de cita especificado no existe.");

        var diagnosticoExiste = await _context.Diagnosticos.AnyAsync(d => d.Id == request.DiagnosticoId && d.Activo);
        if (!diagnosticoExiste)
            return BadRequest("El diagnóstico especificado no existe o se encuentra inactivo.");

        var yaAsignado = await _context.DetallesDiagnostico.AnyAsync(d => 
            d.DetalleCitaId == request.DetalleCitaId && 
            d.DiagnosticoId == request.DiagnosticoId && 
            d.Id != id);
        if (yaAsignado)
            return BadRequest("Este diagnóstico ya está asignado en otro registro para este detalle de cita.");

        // Si se marca como principal, desmarcar los demás diagnósticos del detalle de cita
        if (request.Principal)
        {
            var otrosPrincipales = await _context.DetallesDiagnostico
                .Where(d => d.DetalleCitaId == request.DetalleCitaId && d.Principal && d.Id != id)
                .ToListAsync();

            foreach (var item in otrosPrincipales)
            {
                item.Update(item.DetalleCitaId, item.DiagnosticoId, false);
            }
        }

        detalle.Update(
            detalleCitaId: request.DetalleCitaId,
            diagnosticoId: request.DiagnosticoId,
            principal: request.Principal
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var detalle = await _context.DetallesDiagnostico.FindAsync(id);
        if (detalle is null)
            return NotFound();

        _context.DetallesDiagnostico.Remove(detalle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}