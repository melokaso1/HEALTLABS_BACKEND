using Api.Security;
using Application.DTOs.Medico;
using Domain.Entities;
using Infrastructure.Persistence.Context; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)] 
public sealed class MedicosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MedicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)] 
    public async Task<IActionResult> GetAll()
    {
        var medicos = await _context.Medicos
            .Include(m => m.Persona)
            .Include(m => m.Especialidad)
            .Select(m => new MedicoDto
            {
                IdMedico = m.IdMedico,
                IdPersona = m.IdPersona,
                IdEspecialidad = m.IdEspecialidad
            })
            .ToListAsync();

        return Ok(medicos);
    }

    // GET: api/Medicos/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var medico = await _context.Medicos
            .Where(m => m.IdMedico == id)
            .Select(m => new MedicoDto
            {
                IdMedico = m.IdMedico,
                IdPersona = m.IdPersona,
                IdEspecialidad = m.IdEspecialidad
            })
            .FirstOrDefaultAsync();

        if (medico is null)
            return NotFound();

        return Ok(medico);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicoDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var especialidadExiste = await _context.Especialidades.AnyAsync(e => e.IdEspecialidad == request.IdEspecialidad);
        if (!especialidadExiste)
            return BadRequest("La especialidad especificada no existe.");

        var yaEsMedico = await _context.Medicos.AnyAsync(m => m.IdPersona == request.IdPersona);
        if (yaEsMedico)
            return BadRequest("Esta persona ya está registrada como médico.");

        var nuevoMedico = new Medico
        {
            IdMedico = Guid.NewGuid(),
            IdPersona = request.IdPersona,
            IdEspecialidad = request.IdEspecialidad
        };

        _context.Medicos.Add(nuevoMedico);
        await _context.SaveChangesAsync();

        var medicoDto = new MedicoDto
        {
            IdMedico = nuevoMedico.IdMedico,
            IdPersona = nuevoMedico.IdPersona,
            IdEspecialidad = nuevoMedico.IdEspecialidad
        };

        return CreatedAtAction(nameof(GetById), new { id = medicoDto.IdMedico }, medicoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateMedicoDto request)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var especialidadExiste = await _context.Especialidades.AnyAsync(e => e.IdEspecialidad == request.IdEspecialidad);
        if (!especialidadExiste)
            return BadRequest("La especialidad especificada no existe.");

        medico.IdPersona = request.IdPersona;
        medico.IdEspecialidad = request.IdEspecialidad;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico is null)
            return NotFound();

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}