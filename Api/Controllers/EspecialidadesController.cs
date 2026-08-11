using Api.Security;
using Application.DTOs.Especialidad;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class EspecialidadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EspecialidadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var especialidades = await _context.Especialidades
            .Select(e => new CreateEspecialidadDto
            {
                Nombre = e.Nombre,
                Descripcion = e.Descripcion
            })
            .ToListAsync();

        return Ok(especialidades);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var especialidad = await _context.Especialidades
            .Where(e => e.Id == id)
            .Select(e => new CreateEspecialidadDto
            {
                Nombre = e.Nombre,
                Descripcion = e.Descripcion
            })
            .FirstOrDefaultAsync();

        if (especialidad is null)
            return NotFound();

        return Ok(especialidad);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEspecialidadDto request)
    {
        var nuevaEspecialidad = new EspecialidadEntity(
            nombre: request.Nombre,
            descripcion: request.Descripcion
        );

        _context.Especialidades.Add(nuevaEspecialidad);
        await _context.SaveChangesAsync();

        var especialidadDto = new CreateEspecialidadDto
        {
            Nombre = nuevaEspecialidad.Nombre,
            Descripcion = nuevaEspecialidad.Descripcion
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaEspecialidad.Id }, especialidadDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEspecialidadDto request)
    {
        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad is null)
            return NotFound();

        especialidad.Update(
            nombre: request.Nombre,
            descripcion: request.Descripcion
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad is null)
            return NotFound();

        _context.Especialidades.Remove(especialidad);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}