using Api.Security;
using Application.DTOs.DetalleCita;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class DetallesCitaController : ControllerBase
{
    private readonly AppDbContext _context;

    public DetallesCitaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("cita/{idCita:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByCita(Guid idCita)
    {
        var detalles = await _context.DetallesCita
            .Where(d => d.CitaId == idCita)
            .Select(d => new CreateDetalleCitaDto
            {
                CitaId = d.CitaId,
                MedicoId = d.MedicoId,
                NotaAtencion = d.NotaAtencion,
                ResumenConsulta = d.ResumenConsulta
            })
            .ToListAsync();

        return Ok(detalles);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var detalle = await _context.DetallesCita
            .Where(d => d.Id == id)
            .Select(d => new CreateDetalleCitaDto
            {
                CitaId = d.CitaId,
                MedicoId = d.MedicoId,
                NotaAtencion = d.NotaAtencion,
                ResumenConsulta = d.ResumenConsulta
            })
            .FirstOrDefaultAsync();

        if (detalle is null)
            return NotFound();

        return Ok(detalle);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDetalleCitaDto request)
    {
        var cita = await _context.Citas.FindAsync(request.CitaId);
        if (cita is null)
            return BadRequest("La cita especificada no existe.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe.");

        var nuevoDetalle = new DetalleCitaEntity(
            citaId: request.CitaId,
            medicoId: request.MedicoId,
            notaAtencion: request.NotaAtencion,
            resumenConsulta: request.ResumenConsulta
        );

        _context.DetallesCita.Add(nuevoDetalle);
        await _context.SaveChangesAsync();

        var detalleDto = new CreateDetalleCitaDto
        {
            CitaId = nuevoDetalle.CitaId,
            MedicoId = nuevoDetalle.MedicoId,
            NotaAtencion = nuevoDetalle.NotaAtencion,
            ResumenConsulta = nuevoDetalle.ResumenConsulta
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoDetalle.Id }, detalleDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDetalleCitaDto request)
    {
        var detalle = await _context.DetallesCita.FindAsync(id);
        if (detalle is null)
            return NotFound();

        var citaExiste = await _context.Citas.AnyAsync(c => c.Id == request.CitaId);
        if (!citaExiste)
            return BadRequest("La cita especificada no existe.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe.");

        detalle.Update(
            citaId: request.CitaId,
            medicoId: request.MedicoId,
            notaAtencion: request.NotaAtencion,
            resumenConsulta: request.ResumenConsulta
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var detalle = await _context.DetallesCita.FindAsync(id);
        if (detalle is null)
            return NotFound();

        _context.DetallesCita.Remove(detalle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}