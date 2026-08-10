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
    private readonly ApplicationDbContext _context;

    public PacientesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pacientes = await _context.Pacientes
            .Include(p => p.Persona)
            .Select(p => new PacienteDto
            {
                IdPaciente = p.IdPaciente,
                IdPersona = p.IdPersona,
                Activo = p.Activo
            })
            .ToListAsync();

        return Ok(pacientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var paciente = await _context.Pacientes
            .Where(p => p.IdPaciente == id)
            .Select(p => new PacienteDto
            {
                IdPaciente = p.IdPaciente,
                IdPersona = p.IdPersona,
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
        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe en la base de datos.");

        var yaEsPaciente = await _context.Pacientes.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (yaEsPaciente)
            return BadRequest("Esta persona ya está registrada como paciente.");

        var nuevoPaciente = new Paciente
        {
            IdPaciente = Guid.NewGuid(),
            IdPersona = request.IdPersona,
            Activo = true
        };

        _context.Pacientes.Add(nuevoPaciente);
        await _context.SaveChangesAsync();

        var pacienteDto = new PacienteDto
        {
            IdPaciente = nuevoPaciente.IdPaciente,
            IdPersona = nuevoPaciente.IdPersona,
            Activo = nuevoPaciente.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = pacienteDto.IdPaciente }, pacienteDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreatePacienteDto request)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.IdPersona == request.IdPersona);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        paciente.IdPersona = request.IdPersona;

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

        paciente.Activo = false;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}