using Api.Security;
using Application.DTOs.Tratamiento;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class TratamientosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TratamientosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var tratamientos = await _context.Tratamientos
            .Select(t => new CreateTratamientoDto
            {
                Codigo = t.Codigo,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Activo = t.Activo
            })
            .ToListAsync();

        return Ok(tratamientos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tratamiento = await _context.Tratamientos
            .Where(t => t.Id == id)
            .Select(t => new CreateTratamientoDto
            {
                Codigo = t.Codigo,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Activo = t.Activo
            })
            .FirstOrDefaultAsync();

        if (tratamiento is null)
            return NotFound();

        return Ok(tratamiento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTratamientoDto request)
    {
        var nuevoTratamiento = new TratamientoEntity(
            codigo: request.Codigo,
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        _context.Tratamientos.Add(nuevoTratamiento);
        await _context.SaveChangesAsync();

        var tratamientoResponse = new CreateTratamientoDto
        {
            Codigo = nuevoTratamiento.Codigo,
            Nombre = nuevoTratamiento.Nombre,
            Descripcion = nuevoTratamiento.Descripcion,
            Activo = nuevoTratamiento.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoTratamiento.Id }, tratamientoResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTratamientoDto request)
    {
        var tratamiento = await _context.Tratamientos.FindAsync(id);
        if (tratamiento is null)
            return NotFound();

        tratamiento.Update(
            codigo: request.Codigo,
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tratamiento = await _context.Tratamientos.FindAsync(id);
        if (tratamiento is null)
            return NotFound();

        tratamiento.Update(
            codigo: tratamiento.Codigo,
            nombre: tratamiento.Nombre,
            descripcion: tratamiento.Descripcion,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}