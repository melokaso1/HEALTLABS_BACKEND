using Api.Security;
using Application.DTOs.MedicoEspecialidad;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class MedicoEspecialidadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicoEspecialidadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var medicoEspecialidades = await _context.MedicosEspecialidad
            .Include(me => me.Medico)
            .Include(me => me.Especialidad)
            .Select(me => new CreateMedicoEspecialidadDto
            {
                MedicoId = me.MedicoId,
                EspecialidadId = me.EspecialidadId,
                Principal = me.Principal
            })
            .ToListAsync();

        return Ok(medicoEspecialidades);
    }

    [HttpGet("medico/{medicoId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByMedico(Guid medicoId)
    {
        var especialidades = await _context.MedicosEspecialidad
            .Include(me => me.Especialidad)
            .Where(me => me.MedicoId == medicoId)
            .Select(me => new CreateMedicoEspecialidadDto
            {
                MedicoId = me.MedicoId,
                EspecialidadId = me.EspecialidadId,
                Principal = me.Principal
            })
            .ToListAsync();

        return Ok(especialidades);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var medicoEspecialidad = await _context.MedicosEspecialidad
            .Where(me => me.Id == id)
            .Select(me => new CreateMedicoEspecialidadDto
            {
                MedicoId = me.MedicoId,
                EspecialidadId = me.EspecialidadId,
                Principal = me.Principal
            })
            .FirstOrDefaultAsync();

        if (medicoEspecialidad is null)
            return NotFound();

        return Ok(medicoEspecialidad);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicoEspecialidadDto request)
    {
        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe o se encuentra inactivo.");

        var especialidadExiste = await _context.Especialidades.AnyAsync(e => e.Id == request.EspecialidadId);
        if (!especialidadExiste)
            return BadRequest("La especialidad especificada no existe.");

        var yaAsignada = await _context.MedicosEspecialidad.AnyAsync(me => 
            me.MedicoId == request.MedicoId && 
            me.EspecialidadId == request.EspecialidadId);
        if (yaAsignada)
            return BadRequest("Esta especialidad ya se encuentra asignada al médico especificado.");

        if (request.Principal)
        {
            var otrasPrincipales = await _context.MedicosEspecialidad
                .Where(me => me.MedicoId == request.MedicoId && me.Principal)
                .ToListAsync();

            foreach (var item in otrasPrincipales)
            {
                item.Update(item.MedicoId, item.EspecialidadId, false);
            }
        }

        var nuevaAsignacion = new MedicoEspecialidadEntity(
            medicoId: request.MedicoId,
            especialidadId: request.EspecialidadId,
            principal: request.Principal
        );

        _context.MedicosEspecialidad.Add(nuevaAsignacion);
        await _context.SaveChangesAsync();

        var medicoEspecialidadDto = new CreateMedicoEspecialidadDto
        {
            MedicoId = nuevaAsignacion.MedicoId,
            EspecialidadId = nuevaAsignacion.EspecialidadId,
            Principal = nuevaAsignacion.Principal
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaAsignacion.Id }, medicoEspecialidadDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicoEspecialidadDto request)
    {
        var medicoEspecialidad = await _context.MedicosEspecialidad.FindAsync(id);
        if (medicoEspecialidad is null)
            return NotFound();

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == request.MedicoId && m.Activo);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe o se encuentra inactivo.");

        var especialidadExiste = await _context.Especialidades.AnyAsync(e => e.Id == request.EspecialidadId);
        if (!especialidadExiste)
            return BadRequest("La especialidad especificada no existe.");

        var yaAsignada = await _context.MedicosEspecialidad.AnyAsync(me => 
            me.MedicoId == request.MedicoId && 
            me.EspecialidadId == request.EspecialidadId && 
            me.Id != id);
        if (yaAsignada)
            return BadRequest("Esta especialidad ya se encuentra asignada al médico en otro registro.");

        if (request.Principal)
        {
            var otrasPrincipales = await _context.MedicosEspecialidad
                .Where(me => me.MedicoId == request.MedicoId && me.Principal && me.Id != id)
                .ToListAsync();

            foreach (var item in otrasPrincipales)
            {
                item.Update(item.MedicoId, item.EspecialidadId, false);
            }
        }

        medicoEspecialidad.Update(
            medicoId: request.MedicoId,
            especialidadId: request.EspecialidadId,
            principal: request.Principal
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var medicoEspecialidad = await _context.MedicosEspecialidad.FindAsync(id);
        if (medicoEspecialidad is null)
            return NotFound();

        _context.MedicosEspecialidad.Remove(medicoEspecialidad);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}