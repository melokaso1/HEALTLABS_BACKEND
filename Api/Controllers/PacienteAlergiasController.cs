using Api.Security;
using Application.DTOs.PacienteAlergia;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PacientesAlergiaController : ControllerBase
{
    private readonly AppDbContext _context;

    public PacientesAlergiaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var alergias = await _context.PacientesAlergia
            .Include(a => a.Paciente)
            .Select(a => new CreatePacienteAlergiaDto
            {
                PacienteId = a.PacienteId,
                Sustancia = a.Sustancia,
                Reaccion = a.Reaccion,
                Severidad = a.Severidad,
                Activo = a.Activo
            })
            .ToListAsync();

        return Ok(alergias);
    }

    [HttpGet("paciente/{pacienteId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByPaciente(Guid pacienteId)
    {
        var alergias = await _context.PacientesAlergia
            .Where(a => a.PacienteId == pacienteId)
            .Select(a => new CreatePacienteAlergiaDto
            {
                PacienteId = a.PacienteId,
                Sustancia = a.Sustancia,
                Reaccion = a.Reaccion,
                Severidad = a.Severidad,
                Activo = a.Activo
            })
            .ToListAsync();

        return Ok(alergias);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var alergia = await _context.PacientesAlergia
            .Where(a => a.Id == id)
            .Select(a => new CreatePacienteAlergiaDto
            {
                PacienteId = a.PacienteId,
                Sustancia = a.Sustancia,
                Reaccion = a.Reaccion,
                Severidad = a.Severidad,
                Activo = a.Activo
            })
            .FirstOrDefaultAsync();

        if (alergia is null)
            return NotFound();

        return Ok(alergia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePacienteAlergiaDto request)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId);
        if (!pacienteExiste)
            return BadRequest("El paciente especificado no existe.");

        var alergiaExiste = await _context.PacientesAlergia
            .AnyAsync(a => a.PacienteId == request.PacienteId && a.Sustancia.ToLower() == request.Sustancia.ToLower());
        if (alergiaExiste)
            return BadRequest("Esta alergia/sustancia ya se encuentra registrada para este paciente.");

        var nuevaAlergia = new PacienteAlergiaEntity(
            pacienteId: request.PacienteId,
            sustancia: request.Sustancia,
            reaccion: request.Reaccion,
            severidad: request.Severidad,
            activo: request.Activo
        );

        _context.PacientesAlergia.Add(nuevaAlergia);
        await _context.SaveChangesAsync();

        var alergiaDto = new CreatePacienteAlergiaDto
        {
            PacienteId = nuevaAlergia.PacienteId,
            Sustancia = nuevaAlergia.Sustancia,
            Reaccion = nuevaAlergia.Reaccion,
            Severidad = nuevaAlergia.Severidad,
            Activo = nuevaAlergia.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaAlergia.Id }, alergiaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePacienteAlergiaDto request)
    {
        var alergia = await _context.PacientesAlergia.FindAsync(id);
        if (alergia is null)
            return NotFound();

        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId);
        if (!pacienteExiste)
            return BadRequest("El paciente especificado no existe.");

        var alergiaEnUso = await _context.PacientesAlergia
            .AnyAsync(a => a.PacienteId == request.PacienteId && 
                           a.Sustancia.ToLower() == request.Sustancia.ToLower() && 
                           a.Id != id);
        if (alergiaEnUso)
            return BadRequest("Esta sustancia ya está registrada en otra entrada para este paciente.");

        alergia.Update(
            pacienteId: request.PacienteId,
            sustancia: request.Sustancia,
            reaccion: request.Reaccion,
            severidad: request.Severidad,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var alergia = await _context.PacientesAlergia.FindAsync(id);
        if (alergia is null)
            return NotFound();

        alergia.Update(
            pacienteId: alergia.PacienteId,
            sustancia: alergia.Sustancia,
            reaccion: alergia.Reaccion,
            severidad: alergia.Severidad,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}