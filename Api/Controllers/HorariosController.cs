using Api.Security;
using Application.DTOs.Horario;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class HorariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public HorariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var horarios = await _context.Horarios
            .Include(h => h.Medico)
            .Select(h => new CreateHorarioDto
            {
                MedicoId = h.MedicoId,
                Fecha = h.Fecha,
                HoraEntrada = h.HoraEntrada,
                HoraSalida = h.HoraSalida,
                SalidaAlmuerzo = h.SalidaAlmuerzo,
                RetornoActividades = h.RetornoActividades
            })
            .ToListAsync();

        return Ok(horarios);
    }

    [HttpGet("medico/{medicoId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByMedico(Guid medicoId)
    {
        var horarios = await _context.Horarios
            .Where(h => h.MedicoId == medicoId)
            .Select(h => new CreateHorarioDto
            {
                MedicoId = h.MedicoId,
                Fecha = h.Fecha,
                HoraEntrada = h.HoraEntrada,
                HoraSalida = h.HoraSalida,
                SalidaAlmuerzo = h.SalidaAlmuerzo,
                RetornoActividades = h.RetornoActividades
            })
            .ToListAsync();

        return Ok(horarios);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var horario = await _context.Horarios
            .Where(h => h.Id == id)
            .Select(h => new CreateHorarioDto
            {
                MedicoId = h.MedicoId,
                Fecha = h.Fecha,
                HoraEntrada = h.HoraEntrada,
                HoraSalida = h.HoraSalida,
                SalidaAlmuerzo = h.SalidaAlmuerzo,
                RetornoActividades = h.RetornoActividades
            })
            .FirstOrDefaultAsync();

        if (horario is null)
            return NotFound();

        return Ok(horario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHorarioDto request)
    {
        if (request.HoraEntrada >= request.HoraSalida)
            return BadRequest("La hora de entrada debe ser menor a la hora de salida.");

        if (request.SalidaAlmuerzo >= request.RetornoActividades)
            return BadRequest("La salida a almuerzo debe ser menor al retorno de actividades.");

        if (request.SalidaAlmuerzo < request.HoraEntrada || request.RetornoActividades > request.HoraSalida)
            return BadRequest("El horario de almuerzo debe estar dentro de la jornada laboral.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe o está inactivo.");

        var horarioExiste = await _context.Horarios.AnyAsync(h => h.MedicoId == request.MedicoId && h.Fecha == request.Fecha);
        if (horarioExiste)
            return BadRequest("Ya existe un horario programado para este médico en la fecha indicada.");

        var nuevoHorario = new HorarioEntity(
            medicoId: request.MedicoId,
            fecha: request.Fecha,
            horaEntrada: request.HoraEntrada,
            horaSalida: request.HoraSalida,
            salidaAlmuerzo: request.SalidaAlmuerzo,
            retornoActividades: request.RetornoActividades
        );

        _context.Horarios.Add(nuevoHorario);
        await _context.SaveChangesAsync();

        var horarioDto = new CreateHorarioDto
        {
            MedicoId = nuevoHorario.MedicoId,
            Fecha = nuevoHorario.Fecha,
            HoraEntrada = nuevoHorario.HoraEntrada,
            HoraSalida = nuevoHorario.HoraSalida,
            SalidaAlmuerzo = nuevoHorario.SalidaAlmuerzo,
            RetornoActividades = nuevoHorario.RetornoActividades
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoHorario.Id }, horarioDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHorarioDto request)
    {
        var horario = await _context.Horarios.FindAsync(id);
        if (horario is null)
            return NotFound();

        if (request.HoraEntrada >= request.HoraSalida)
            return BadRequest("La hora de entrada debe ser menor a la hora de salida.");

        if (request.SalidaAlmuerzo >= request.RetornoActividades)
            return BadRequest("La salida a almuerzo debe ser menor al retorno de actividades.");

        if (request.SalidaAlmuerzo < request.HoraEntrada || request.RetornoActividades > request.HoraSalida)
            return BadRequest("El horario de almuerzo debe estar dentro de la jornada laboral.");

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe o está inactivo.");

        var fechaEnUso = await _context.Horarios.AnyAsync(h => h.MedicoId == request.MedicoId && h.Fecha == request.Fecha && h.Id != id);
        if (fechaEnUso)
            return BadRequest("Ya existe otro horario registrado para este médico en la fecha indicada.");

        horario.Update(
            medicoId: request.MedicoId,
            fecha: request.Fecha,
            horaEntrada: request.HoraEntrada,
            horaSalida: request.HoraSalida,
            salidaAlmuerzo: request.SalidaAlmuerzo,
            retornoActividades: request.RetornoActividades
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var horario = await _context.Horarios.FindAsync(id);
        if (horario is null)
            return NotFound();

        _context.Horarios.Remove(horario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}