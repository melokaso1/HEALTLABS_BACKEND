using Api.Security;
using Application.DTOs.DetalleCita;
using Domain.Entities;
using Infrastructure.Persistence; // Ajusta la ruta según tu DbContext
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)] 
public sealed class DetallesCitaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DetallesCitaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("cita/{idCita:guid}")]
    [Authorize(Roles = AppRoles.Todos)] 
    public async Task<IActionResult> GetByCita(Guid idCita)
    {
        var detalles = await _context.DetallesCita
            .Where(d => d.IdCita == idCita)
            .Select(d => new DetalleCitaDto
            {
                IdDetalleCita = d.IdDetalleCita,
                Observaciones = d.Observaciones,
                IdCita = d.IdCita,
                IdDiagnostico = d.IdDiagnostico
            })
            .ToListAsync();

        return Ok(detalles);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var detalle = await _context.DetallesCita
            .Where(d => d.IdDetalleCita == id)
            .Select(d => new DetalleCitaDto
            {
                IdDetalleCita = d.IdDetalleCita,
                Observaciones = d.Observaciones,
                IdCita = d.IdCita,
                IdDiagnostico = d.IdDiagnostico
            })
            .FirstOrDefaultAsync();

        if (detalle is null)
            return NotFound();

        return Ok(detalle);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDetalleCitaDto request)
    {
        var cita = await _context.Citas.Include(c => c.EstadoCita).FirstOrDefaultAsync(c => c.IdCita == request.IdCita);
        if (cita is null)
            return BadRequest("La cita especificada no existe.");

        var diagnosticoExiste = await _context.Diagnosticos.AnyAsync(d => d.IdDiagnostico == request.IdDiagnostico);
        if (!diagnosticoExiste)
            return BadRequest("El diagnóstico especificado no existe.");

        var nuevoDetalle = new DetalleCita
        {
            IdDetalleCita = Guid.NewGuid(),
            Observaciones = request.Observaciones,
            IdCita = request.IdCita,
            IdDiagnostico = request.IdDiagnostico
        };

        _context.DetallesCita.Add(nuevoDetalle);

        var estadoAtendida = await _context.EstadosCita.FirstOrDefaultAsync(e => e.Nombre == "Atendida");
        if (estadoAtendida != null)
        {
            cita.IdEstadoCita = estadoAtendida.IdEstadoCita;
        }

        await _context.SaveChangesAsync();

        var detalleDto = new DetalleCitaDto
        {
            IdDetalleCita = nuevoDetalle.IdDetalleCita,
            Observaciones = nuevoDetalle.Observaciones,
            IdCita = nuevoDetalle.IdCita,
            IdDiagnostico = nuevoDetalle.IdDiagnostico
        };

        return CreatedAtAction(nameof(GetById), new { id = detalleDto.IdDetalleCita }, detalleDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateDetalleCitaDto request)
    {
        var detalle = await _context.DetallesCita.FindAsync(id);
        if (detalle is null)
            return NotFound();

        detalle.Observaciones = request.Observaciones;
        detalle.IdDiagnostico = request.IdDiagnostico;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}