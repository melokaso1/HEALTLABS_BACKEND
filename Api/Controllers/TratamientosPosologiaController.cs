using Api.Security;
using Application.DTOs.TratamientoPosologia;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class TratamientosPosologiaController : ControllerBase
{
    private readonly AppDbContext _context;

    public TratamientosPosologiaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var posologias = await _context.TratamientosPosologia
            .Include(tp => tp.Tratamiento)
            .Select(tp => new CreateTratamientoPosologiaDto
            {
                TratamientoId = tp.TratamientoId,
                Dosis = tp.Dosis,
                Frecuencia = tp.Frecuencia,
                DuracionDias = tp.DuracionDias,
                Indicaciones = tp.Indicaciones
            })
            .ToListAsync();

        return Ok(posologias);
    }

    [HttpGet("tratamiento/{tratamientoId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByTratamiento(Guid tratamientoId)
    {
        var posologias = await _context.TratamientosPosologia
            .Where(tp => tp.TratamientoId == tratamientoId)
            .Select(tp => new CreateTratamientoPosologiaDto
            {
                TratamientoId = tp.TratamientoId,
                Dosis = tp.Dosis,
                Frecuencia = tp.Frecuencia,
                DuracionDias = tp.DuracionDias,
                Indicaciones = tp.Indicaciones
            })
            .ToListAsync();

        return Ok(posologias);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var posologia = await _context.TratamientosPosologia
            .Where(tp => tp.Id == id)
            .Select(tp => new CreateTratamientoPosologiaDto
            {
                TratamientoId = tp.TratamientoId,
                Dosis = tp.Dosis,
                Frecuencia = tp.Frecuencia,
                DuracionDias = tp.DuracionDias,
                Indicaciones = tp.Indicaciones
            })
            .FirstOrDefaultAsync();

        if (posologia is null)
            return NotFound();

        return Ok(posologia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTratamientoPosologiaDto request)
    {
        var tratamientoExiste = await _context.Tratamientos.AnyAsync(t => t.Id == request.TratamientoId);
        if (!tratamientoExiste)
            return BadRequest("El tratamiento especificado no existe.");

        if (request.DuracionDias.HasValue && request.DuracionDias.Value <= 0)
            return BadRequest("La duración en días debe ser mayor a 0.");

        var nuevaPosologia = new TratamientoPosologiaEntity(
            tratamientoId: request.TratamientoId,
            dosis: request.Dosis,
            frecuencia: request.Frecuencia,
            duracionDias: request.DuracionDias,
            indicaciones: request.Indicaciones
        );

        _context.TratamientosPosologia.Add(nuevaPosologia);
        await _context.SaveChangesAsync();

        var posologiaDto = new CreateTratamientoPosologiaDto
        {
            TratamientoId = nuevaPosologia.TratamientoId,
            Dosis = nuevaPosologia.Dosis,
            Frecuencia = nuevaPosologia.Frecuencia,
            DuracionDias = nuevaPosologia.DuracionDias,
            Indicaciones = nuevaPosologia.Indicaciones
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaPosologia.Id }, posologiaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTratamientoPosologiaDto request)
    {
        var posologia = await _context.TratamientosPosologia.FindAsync(id);
        if (posologia is null)
            return NotFound();

        var tratamientoExiste = await _context.Tratamientos.AnyAsync(t => t.Id == request.TratamientoId);
        if (!tratamientoExiste)
            return BadRequest("El tratamiento especificado no existe.");

        if (request.DuracionDias.HasValue && request.DuracionDias.Value <= 0)
            return BadRequest("La duración en días debe ser mayor a 0.");

        posologia.Update(
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
        var posologia = await _context.TratamientosPosologia.FindAsync(id);
        if (posologia is null)
            return NotFound();

        _context.TratamientosPosologia.Remove(posologia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}