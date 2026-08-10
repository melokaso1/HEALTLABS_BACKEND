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
    private readonly ApplicationDbContext _context;

    public EspecialidadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous] 
    public async Task<IActionResult> GetAll()
    {
        var especialidades = await _context.Especialidades
            .Select(e => new EspecialidadDto
            {
                IdEspecialidad = e.IdEspecialidad,
                Nombre = e.Nombre
            })
            .ToListAsync();

        return Ok(especialidades);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var especialidad = await _context.Especialidades
            .Where(e => e.IdEspecialidad == id)
            .Select(e => new EspecialidadDto
            {
                IdEspecialidad = e.IdEspecialidad,
                Nombre = e.Nombre
            })
            .FirstOrDefaultAsync();

        if (especialidad is null)
            return NotFound();

        return Ok(especialidad);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEspecialidadDto request)
    {
        var nuevaEspecialidad = new Especialidad
        {
            IdEspecialidad = Guid.NewGuid(),
            Nombre = request.Nombre
        };

        _context.Especialidades.Add(nuevaEspecialidad);
        await _context.SaveChangesAsync();

        var especialidadDto = new EspecialidadDto
        {
            IdEspecialidad = nuevaEspecialidad.IdEspecialidad,
            Nombre = nuevaEspecialidad.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = especialidadDto.IdEspecialidad }, especialidadDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateEspecialidadDto request)
    {
        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad is null)
            return NotFound();

        especialidad.Nombre = request.Nombre;

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