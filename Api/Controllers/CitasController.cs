using Api.Security;
using Application.DTOs.Cita;
using Domain.Entities;
using Infrastructure.Persistence.Context; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)] 
public sealed class CitasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CitasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var citas = await _context.Citas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.EstadoCita)
            .Select(c => new CitaDto
            {
                IdCita = c.IdCita,
                FechaHora = c.FechaHora,
                Motivo = c.Motivo,
                IdPaciente = c.IdPaciente,
                IdMedico = c.IdMedico,
                IdEstadoCita = c.IdEstadoCita
            })
            .ToListAsync();

        return Ok(citas);
    }

    // GET: api/Citas/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cita = await _context.Citas
            .Where(c => c.IdCita == id)
            .Select(c => new CitaDto
            {
                IdCita = c.IdCita,
                FechaHora = c.FechaHora,
                Motivo = c.Motivo,
                IdPaciente = c.IdPaciente,
                IdMedico = c.IdMedico,
                IdEstadoCita = c.IdEstadoCita
            })
            .FirstOrDefaultAsync();

        if (cita is null)
            return NotFound();

        return Ok(cita);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.IdPaciente == request.IdPaciente && p.Activo);
        if (!pacienteExiste)
            return BadRequest("El paciente no existe o se encuentra inactivo.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.IdMedico == request.IdMedico);
        if (!medicoExiste)
            return BadRequest("El médico no existe.");

        var estadoExiste = await _context.EstadosCita.AnyAsync(e => e.IdEstadoCita == request.IdEstadoCita);
        if (!estadoExiste)
            return BadRequest("El estado de cita especificado no existe.");

        var cruceHorario = await _context.Citas.AnyAsync(c => 
            c.IdMedico == request.IdMedico && 
            c.FechaHora == request.FechaHora &&
            c.EstadoCita.Nombre != "Cancelada"
        );

        if (cruceHorario)
            return BadRequest("El profesional ya tiene una cita agendada para esa fecha y hora.");

        var nuevaCita = new Cita
        {
            IdCita = Guid.NewGuid(),
            FechaHora = request.FechaHora,
            Motivo = request.Motivo,
            IdPaciente = request.IdPaciente,
            IdMedico = request.IdMedico,
            IdEstadoCita = request.IdEstadoCita
        };

        _context.Citas.Add(nuevaCita);
        await _context.SaveChangesAsync();

        var citaDto = new CitaDto
        {
            IdCita = nuevaCita.IdCita,
            FechaHora = nuevaCita.FechaHora,
            Motivo = nuevaCita.Motivo,
            IdPaciente = nuevaCita.IdPaciente,
            IdMedico = nuevaCita.IdMedico,
            IdEstadoCita = nuevaCita.IdEstadoCita
        };

        return CreatedAtAction(nameof(GetById), new { id = citaDto.IdCita }, citaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateCitaDto request)
    {
        var cita = await _context.Citas.FindAsync(id);
        if (cita is null)
            return NotFound();

        if (cita.FechaHora != request.FechaHora || cita.IdMedico != request.IdMedico)
        {
            var cruceHorario = await _context.Citas.AnyAsync(c => 
                c.IdCita != id && 
                c.IdMedico == request.IdMedico && 
                c.FechaHora == request.FechaHora &&
                c.EstadoCita.Nombre != "Cancelada"
            );

            if (cruceHorario)
                return BadRequest("El profesional ya tiene otra cita agendada en la nueva fecha y hora.");
        }

        cita.FechaHora = request.FechaHora;
        cita.Motivo = request.Motivo;
        cita.IdPaciente = request.IdPaciente;
        cita.IdMedico = request.IdMedico;
        cita.IdEstadoCita = request.IdEstadoCita;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cita = await _context.Citas.Include(c => c.EstadoCita).FirstOrDefaultAsync(c => c.IdCita == id);
        if (cita is null)
            return NotFound();

        var estadoCancelado = await _context.EstadosCita.FirstOrDefaultAsync(e => e.Nombre == "Cancelada");
        if (estadoCancelado != null)
        {
            cita.IdEstadoCita = estadoCancelado.IdEstadoCita;
        }
        else
        {
            _context.Citas.Remove(cita); 
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}