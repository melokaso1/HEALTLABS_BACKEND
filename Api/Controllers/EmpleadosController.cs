using Api.Security;
using Application.DTOs.Empleado;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class EmpleadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpleadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empleados = await _context.Empleados
            .Include(e => e.Persona)
            .Include(e => e.Cargo)
            .Select(e => new CreateEmpleadoDto
            {
                PersonaId = e.PersonaId,
                CargoId = e.CargoId,
                FechaIngreso = e.FechaIngreso,
                FechaRetiro = e.FechaRetiro,
                Activo = e.Activo
            })
            .ToListAsync();

        return Ok(empleados);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var empleado = await _context.Empleados
            .Where(e => e.Id == id)
            .Select(e => new CreateEmpleadoDto
            {
                PersonaId = e.PersonaId,
                CargoId = e.CargoId,
                FechaIngreso = e.FechaIngreso,
                FechaRetiro = e.FechaRetiro,
                Activo = e.Activo
            })
            .FirstOrDefaultAsync();

        if (empleado is null)
            return NotFound();

        return Ok(empleado);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmpleadoDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var cargoExiste = await _context.Cargos.AnyAsync(c => c.Id == request.CargoId);
        if (!cargoExiste)
            return BadRequest("El cargo especificado no existe.");

        var yaEsEmpleado = await _context.Empleados.AnyAsync(e => e.PersonaId == request.PersonaId);
        if (yaEsEmpleado)
            return BadRequest("Esta persona ya se encuentra registrada como empleado.");

        var nuevoEmpleado = new EmpleadoEntity(
            personaId: request.PersonaId,
            cargoId: request.CargoId,
            fechaIngreso: request.FechaIngreso,
            fechaRetiro: request.FechaRetiro,
            activo: request.Activo
        );

        _context.Empleados.Add(nuevoEmpleado);
        await _context.SaveChangesAsync();

        var empleadoDto = new CreateEmpleadoDto
        {
            PersonaId = nuevoEmpleado.PersonaId,
            CargoId = nuevoEmpleado.CargoId,
            FechaIngreso = nuevoEmpleado.FechaIngreso,
            FechaRetiro = nuevoEmpleado.FechaRetiro,
            Activo = nuevoEmpleado.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoEmpleado.Id }, empleadoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmpleadoDto request)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var cargoExiste = await _context.Cargos.AnyAsync(c => c.Id == request.CargoId);
        if (!cargoExiste)
            return BadRequest("El cargo especificado no existe.");

        var personaEnUso = await _context.Empleados.AnyAsync(e => e.PersonaId == request.PersonaId && e.Id != id);
        if (personaEnUso)
            return BadRequest("La persona especificada ya está asignada a otro registro de empleado.");

        empleado.Update(
            personaId: request.PersonaId,
            cargoId: request.CargoId,
            fechaIngreso: request.FechaIngreso,
            fechaRetiro: request.FechaRetiro,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado is null)
            return NotFound();

        empleado.Update(
            personaId: empleado.PersonaId,
            cargoId: empleado.CargoId,
            fechaIngreso: empleado.FechaIngreso,
            fechaRetiro: empleado.FechaRetiro ?? DateOnly.FromDateTime(DateTime.UtcNow),
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}