using Api.Security;
using Application.DTOs.Paciente;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PacientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PacientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pacientes = await _context.Pacientes
            .Include(p => p.Persona)
            .Select(p => new CreatePacienteDto
            {
                PersonaId = p.PersonaId,
                Activo = p.Activo
            })
            .ToListAsync();

        return Ok(pacientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var paciente = await _context.Pacientes
            .Where(p => p.Id == id)
            .Select(p => new CreatePacienteDto
            {
                PersonaId = p.PersonaId,
                Activo = p.Activo
            })
            .FirstOrDefaultAsync();

        if (paciente is null)
            return NotFound();

        return Ok(paciente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePacienteDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe en la base de datos.");

        var yaEsPaciente = await _context.Pacientes.AnyAsync(p => p.PersonaId == request.PersonaId);
        if (yaEsPaciente)
            return BadRequest("Esta persona ya está registrada como paciente.");

        var nuevoPaciente = new PacienteEntity(
            personaId: request.PersonaId,
            activo: request.Activo
        );

        _context.Pacientes.Add(nuevoPaciente);
        await _context.SaveChangesAsync();

        var pacienteDto = new CreatePacienteDto
        {
            PersonaId = nuevoPaciente.PersonaId,
            Activo = nuevoPaciente.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoPaciente.Id }, pacienteDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePacienteDto request)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        paciente.Update(
            personaId: request.PersonaId,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente is null)
            return NotFound();

        paciente.Update(
            personaId: paciente.PersonaId,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}