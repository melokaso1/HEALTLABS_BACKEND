using Api.Security;
using Application.DTOs.AtencionTratamiento;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Medico)]
public sealed class AtencionesTratamientoController : ControllerBase
{
    private readonly AppDbContext _context;

    public AtencionesTratamientoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var atenciones = await _context.AtencionesTratamiento
            .Include(at => at.DetalleCita)
            .Include(at => at.Tratamiento)
            .Select(at => new CreateAtencionTratamientoDto
            {
                DetalleCitaId = at.DetalleCitaId,
                TratamientoId = at.TratamientoId,
                Dosis = at.Dosis,
                Frecuencia = at.Frecuencia,
                DuracionDias = at.DuracionDias,
                Indicaciones = at.Indicaciones
            })
            .ToListAsync();

        return Ok(atenciones);
    }

    [HttpGet("detalle-cita/{detalleCitaId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByDetalleCita(Guid detalleCitaId)
    {
        var atenciones = await _context.AtencionesTratamiento
            .Include(at => at.Tratamiento)
            .Where(at => at.DetalleCitaId == detalleCitaId)
            .Select(at => new CreateAtencionTratamientoDto
            {
                DetalleCitaId = at.DetalleCitaId,
                TratamientoId = at.TratamientoId,
                Dosis = at.Dosis,
                Frecuencia = at.Frecuencia,
                DuracionDias = at.DuracionDias,
                Indicaciones = at.Indicaciones
            })
            .ToListAsync();

        return Ok(atenciones);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var atencion = await _context.AtencionesTratamiento
            .Where(at => at.Id == id)
            .Select(at => new CreateAtencionTratamientoDto
            {
                DetalleCitaId = at.DetalleCitaId,
                TratamientoId = at.TratamientoId,
                Dosis = at.Dosis,
                Frecuencia = at.Frecuencia,
                DuracionDias = at.DuracionDias,
                Indicaciones = at.Indicaciones
            })
            .FirstOrDefaultAsync();

        if (atencion is null)
            return NotFound();

        return Ok(atencion);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAtencionTratamientoDto request)
    {
        var detalleCitaExiste = await _context.DetallesCita.AnyAsync(dc => dc.Id == request.DetalleCitaId);
        if (!detalleCitaExiste)
            return BadRequest("El detalle de cita especificado no existe.");

        var tratamientoExiste = await _context.Tratamientos.AnyAsync(t => t.Id == request.TratamientoId);
        if (!tratamientoExiste)
            return BadRequest("El tratamiento especificado no existe.");

        if (request.DuracionDias.HasValue && request.DuracionDias.Value <= 0)
            return BadRequest("La duración en días debe ser mayor a 0.");

        var nuevaAtencion = new AtencionTratamientoEntity(
            detalleCitaId: request.DetalleCitaId,
            tratamientoId: request.TratamientoId,
            dosis: request.Dosis,
            frecuencia: request.Frecuencia,
            duracionDias: request.DuracionDias,
            indicaciones: request.Indicaciones
        );

        _context.AtencionesTratamiento.Add(nuevaAtencion);
        await _context.SaveChangesAsync();

        var atencionDto = new CreateAtencionTratamientoDto
        {
            DetalleCitaId = nuevaAtencion.DetalleCitaId,
            TratamientoId = nuevaAtencion.TratamientoId,
            Dosis = nuevaAtencion.Dosis,
            Frecuencia = nuevaAtencion.Frecuencia,
            DuracionDias = nuevaAtencion.DuracionDias,
            Indicaciones = nuevaAtencion.Indicaciones
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaAtencion.Id }, atencionDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAtencionTratamientoDto request)
    {
        var atencion = await _context.AtencionesTratamiento.FindAsync(id);
        if (atencion is null)
            return NotFound();

        var detalleCitaExiste = await _context.DetallesCita.AnyAsync(dc => dc.Id == request.DetalleCitaId);
        if (!detalleCitaExiste)
            return BadRequest("El detalle de cita especificado no existe.");

        var tratamientoExiste = await _context.Tratamientos.AnyAsync(t => t.Id == request.TratamientoId);
        if (!tratamientoExiste)
            return BadRequest("El tratamiento especificado no existe.");

        if (request.DuracionDias.HasValue && request.DuracionDias.Value <= 0)
            return BadRequest("La duración en días debe ser mayor a 0.");

        atencion.Update(
            detalleCitaId: request.DetalleCitaId,
            tratamientoId: request.TratamientoId,
            dosis: request.Dosis,
            frecuencia: request.Frecuencia,
            duracionDias: request.DuracionDias,
            indicaciones: request.Indicaciones
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var atencion = await _context.AtencionesTratamiento.FindAsync(id);
        if (atencion is null)
            return NotFound();

        _context.AtencionesTratamiento.Remove(atencion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}