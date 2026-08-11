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
    private readonly AppDbContext _context;

    public MedicosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var medicos = await _context.Medicos
            .Include(m => m.Empleado)
            .Select(m => new CreateMedicoDto
            {
                EmpleadoId = m.EmpleadoId,
                RegistroProfesional = m.RegistroProfesional,
                Activo = m.Activo
            })
            .ToListAsync();

        return Ok(medicos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var medico = await _context.Medicos
            .Where(m => m.Id == id)
            .Select(m => new CreateMedicoDto
            {
                EmpleadoId = m.EmpleadoId,
                RegistroProfesional = m.RegistroProfesional,
                Activo = m.Activo
            })
            .FirstOrDefaultAsync();

        if (medico is null)
            return NotFound();

        return Ok(medico);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicoDto request)
    {
        var empleadoExiste = await _context.Empleados.AnyAsync(e => e.Id == request.EmpleadoId);
        if (!empleadoExiste)
            return BadRequest("El empleado especificado no existe.");

        var yaEsMedico = await _context.Medicos.AnyAsync(m => m.EmpleadoId == request.EmpleadoId);
        if (yaEsMedico)
            return BadRequest("Este empleado ya se encuentra registrado como médico.");

        var nuevoMedico = new MedicoEntity(
            empleadoId: request.EmpleadoId,
            registroProfesional: request.RegistroProfesional,
            activo: request.Activo
        );

        _context.Medicos.Add(nuevoMedico);
        await _context.SaveChangesAsync();

        var medicoDto = new CreateMedicoDto
        {
            EmpleadoId = nuevoMedico.EmpleadoId,
            RegistroProfesional = nuevoMedico.RegistroProfesional,
            Activo = nuevoMedico.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoMedico.Id }, medicoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicoDto request)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico is null)
            return NotFound();

        var empleadoExiste = await _context.Empleados.AnyAsync(e => e.Id == request.EmpleadoId);
        if (!empleadoExiste)
            return BadRequest("El empleado especificado no existe.");

        var yaEsMedico = await _context.Medicos.AnyAsync(m => m.EmpleadoId == request.EmpleadoId && m.Id != id);
        if (yaEsMedico)
            return BadRequest("El empleado especificado ya está registrado en otro registro médico.");

        medico.Update(
            empleadoId: request.EmpleadoId,
            registroProfesional: request.RegistroProfesional,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico is null)
            return NotFound();

        medico.Update(
            empleadoId: medico.EmpleadoId,
            registroProfesional: medico.RegistroProfesional,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}