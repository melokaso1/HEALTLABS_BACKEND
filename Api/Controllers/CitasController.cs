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
    private readonly AppDbContext _context;

    public CitasController(AppDbContext context)
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
            .Include(c => c.TipoCita)
            .Select(c => new CreateCitaDto
            {
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                EstadoCitaId = c.EstadoCitaId,
                TipoCitaId = c.TipoCitaId,
                Fecha = c.Fecha,
                HoraInicio = c.HoraInicio,
                HoraFin = c.HoraFin,
                MotivoConsulta = c.MotivoConsulta,
                Observaciones = c.Observaciones,
                UsuarioCreacionId = c.UsuarioCreacionId
            })
            .ToListAsync();

        return Ok(citas);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cita = await _context.Citas
            .Where(c => c.Id == id)
            .Select(c => new CreateCitaDto
            {
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                EstadoCitaId = c.EstadoCitaId,
                TipoCitaId = c.TipoCitaId,
                Fecha = c.Fecha,
                HoraInicio = c.HoraInicio,
                HoraFin = c.HoraFin,
                MotivoConsulta = c.MotivoConsulta,
                Observaciones = c.Observaciones,
                UsuarioCreacionId = c.UsuarioCreacionId
            })
            .FirstOrDefaultAsync();

        if (cita is null)
            return NotFound();

        return Ok(cita);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCitaDto request)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId && p.Activo);
        if (!pacienteExiste)
            return BadRequest("El paciente no existe o se encuentra inactivo.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico no existe o se encuentra inactivo.");

        var estadoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoCitaId);
        if (!estadoExiste)
            return BadRequest("El estado de cita especificado no existe.");

        var tipoExiste = await _context.TiposCita.AnyAsync(t => t.Id == request.TipoCitaId && t.Activo);
        if (!tipoExiste)
            return BadRequest("El tipo de cita especificado no existe o está inactivo.");

        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioCreacionId);
        if (!usuarioExiste)
            return BadRequest("El usuario creador especificado no existe.");

        var cruceHorario = await _context.Citas.AnyAsync(c =>
            c.MedicoId == request.MedicoId &&
            c.Fecha == request.Fecha &&
            c.HoraInicio < request.HoraFin &&
            c.HoraFin > request.HoraInicio &&
            c.EstadoCita!.Codigo != "CANCELADA"
        );

        if (cruceHorario)
            return BadRequest("El médico ya tiene una cita agendada en ese rango horario.");

        var nuevaCita = new CitaEntity(
            pacienteId: request.PacienteId,
            medicoId: request.MedicoId,
            estadoCitaId: request.EstadoCitaId,
            tipoCitaId: request.TipoCitaId,
            fecha: request.Fecha,
            horaInicio: request.HoraInicio,
            horaFin: request.HoraFin,
            motivoConsulta: request.MotivoConsulta,
            observaciones: request.Observaciones,
            usuarioCreacionId: request.UsuarioCreacionId
        );

        _context.Citas.Add(nuevaCita);
        await _context.SaveChangesAsync();

        var citaDto = new CreateCitaDto
        {
            PacienteId = nuevaCita.PacienteId,
            MedicoId = nuevaCita.MedicoId,
            EstadoCitaId = nuevaCita.EstadoCitaId,
            TipoCitaId = nuevaCita.TipoCitaId,
            Fecha = nuevaCita.Fecha,
            HoraInicio = nuevaCita.HoraInicio,
            HoraFin = nuevaCita.HoraFin,
            MotivoConsulta = nuevaCita.MotivoConsulta,
            Observaciones = nuevaCita.Observaciones,
            UsuarioCreacionId = nuevaCita.UsuarioCreacionId
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaCita.Id }, citaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCitaDto request)
    {
        var cita = await _context.Citas.FindAsync(id);
        if (cita is null)
            return NotFound();

        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId && p.Activo);
        if (!pacienteExiste)
            return BadRequest("El paciente no existe o se encuentra inactivo.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico no existe o se encuentra inactivo.");

        var estadoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == request.EstadoCitaId);
        if (!estadoExiste)
            return BadRequest("El estado de cita especificado no existe.");

        var tipoExiste = await _context.TiposCita.AnyAsync(t => t.Id == request.TipoCitaId && t.Activo);
        if (!tipoExiste)
            return BadRequest("El tipo de cita especificado no existe o está inactivo.");

        var cruceHorario = await _context.Citas.AnyAsync(c =>
            c.Id != id &&
            c.MedicoId == request.MedicoId &&
            c.Fecha == request.Fecha &&
            c.HoraInicio < request.HoraFin &&
            c.HoraFin > request.HoraInicio &&
            c.EstadoCita!.Codigo != "CANCELADA"
        );

        if (cruceHorario)
            return BadRequest("El médico ya tiene otra cita agendada en ese nuevo horario.");

        cita.Update(
            pacienteId: request.PacienteId,
            medicoId: request.MedicoId,
            estadoCitaId: request.EstadoCitaId,
            tipoCitaId: request.TipoCitaId,
            fecha: request.Fecha,
            horaInicio: request.HoraInicio,
            horaFin: request.HoraFin,
            motivoConsulta: request.MotivoConsulta,
            observaciones: request.Observaciones,
            usuarioCreacionId: cita.UsuarioCreacionId,
            motivoCancelacion: request.MotivoCancelacion ?? cita.MotivoCancelacion,
            usuarioCancelacionId: request.UsuarioCancelacionId ?? cita.UsuarioCancelacionId,
            fechaCancelacion: request.FechaCancelacion ?? cita.FechaCancelacion
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] string motivoCancelacion = "Cancelada por usuario")
    {
        var cita = await _context.Citas.Include(c => c.EstadoCita).FirstOrDefaultAsync(c => c.Id == id);
        if (cita is null)
            return NotFound();

        var estadoCancelada = await _context.EstadosCita.FirstOrDefaultAsync(e => e.Codigo == "CANCELADA" || e.Codigo == "Cancelada");

        cita.Update(
            pacienteId: cita.PacienteId,
            medicoId: cita.MedicoId,
            estadoCitaId: estadoCancelada?.Id ?? cita.EstadoCitaId,
            tipoCitaId: cita.TipoCitaId,
            fecha: cita.Fecha,
            horaInicio: cita.HoraInicio,
            horaFin: cita.HoraFin,
            motivoConsulta: cita.MotivoConsulta,
            observaciones: cita.Observaciones,
            usuarioCreacionId: cita.UsuarioCreacionId,
            motivoCancelacion: motivoCancelacion,
            usuarioCancelacionId: cita.UsuarioCreacionId,
            fechaCancelacion: DateTime.UtcNow
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}