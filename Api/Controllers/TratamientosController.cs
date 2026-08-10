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
    private readonly ApplicationDbContext _context;

    public TratamientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var tratamientos = await _context.Tratamientos
            .Select(t => new TratamientoDto
            {
                IdTratamiento = t.IdTratamiento,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            })
            .ToListAsync();

        return Ok(tratamientos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tratamiento = await _context.Tratamientos
            .Where(t => t.IdTratamiento == id)
            .Select(t => new TratamientoDto
            {
                IdTratamiento = t.IdTratamiento,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            })
            .FirstOrDefaultAsync();

        if (tratamiento is null)
            return NotFound();

        return Ok(tratamiento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTratamientoDto request)
    {
        var nuevoTratamiento = new Tratamiento
        {
            IdTratamiento = Guid.NewGuid(),
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        _context.Tratamientos.Add(nuevoTratamiento);
        await _context.SaveChangesAsync();

        var tratamientoDto = new TratamientoDto
        {
            IdTratamiento = nuevoTratamiento.IdTratamiento,
            Nombre = nuevoTratamiento.Nombre,
            Descripcion = nuevoTratamiento.Descripcion
        };

        return CreatedAtAction(nameof(GetById), new { id = tratamientoDto.IdTratamiento }, tratamientoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateTratamientoDto request)
    {
        var tratamiento = await _context.Tratamientos.FindAsync(id);
        if (tratamiento is null)
            return NotFound();

        tratamiento.Nombre = request.Nombre;
        tratamiento.Descripcion = request.Descripcion;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tratamiento = await _context.Tratamientos.FindAsync(id);
        if (tratamiento is null)
            return NotFound();

        _context.Tratamientos.Remove(tratamiento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}