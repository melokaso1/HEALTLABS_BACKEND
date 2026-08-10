using Api.Security;
using Application.DTOs.Disponibilidad;
using Domain.Entities;
using Infrastructure.Persistence.Context; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)] 
public sealed class DisponibilidadesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DisponibilidadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)] 
    public async Task<IActionResult> GetAll()
    {
        var disponibilidades = await _context.Disponibilidades
            .Include(d => d.Medico)
            .Select(d => new DisponibilidadDto
            {
                IdDisponibilidad = d.IdDisponibilidad,
                DiaSemana = d.DiaSemana,
                HoraInicio = d.HoraInicio,
                HoraFin = d.HoraFin,
                IdMedico = d.IdMedico
            })
            .ToListAsync();

        return Ok(disponibilidades);
    }

    // GET: api/Disponibilidades/medico/{idMedico}
    [HttpGet("medico/{idMedico:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByMedico(Guid idMedico)
    {
        var disponibilidades = await _context.Disponibilidades
            .Where(d => d.IdMedico == idMedico)
            .Select(d => new DisponibilidadDto
            {
                IdDisponibilidad = d.IdDisponibilidad,
                DiaSemana = d.DiaSemana,
                HoraInicio = d.HoraInicio,
                HoraFin = d.HoraFin,
                IdMedico = d.IdMedico
            })
            .ToListAsync();

        return Ok(disponibilidades);
    }

    // GET: api/Disponibilidades/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var disponibilidad = await _context.Disponibilidades
            .Where(d => d.IdDisponibilidad == id)
            .Select(d => new DisponibilidadDto
            {
                IdDisponibilidad = d.IdDisponibilidad,
                DiaSemana = d.DiaSemana,
                HoraInicio = d.HoraInicio,
                HoraFin = d.HoraFin,
                IdMedico = d.IdMedico
            })
            .FirstOrDefaultAsync();

        if (disponibilidad is null)
            return NotFound();

        return Ok(disponibilidad);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDisponibilidadDto request)
    {
        var medicoExiste = await _context.Medicos.AnyAsync(m => m.IdMedico == request.IdMedico);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe.");

        var nuevaDisponibilidad = new Disponibilidad
        {
            IdDisponibilidad = Guid.NewGuid(),
            DiaSemana = request.DiaSemana,
            HoraInicio = request.HoraInicio,
            HoraFin = request.HoraFin,
            IdMedico = request.IdMedico
        };

        _context.Disponibilidades.Add(nuevaDisponibilidad);
        await _context.SaveChangesAsync();

        var disponibilidadDto = new DisponibilidadDto
        {
            IdDisponibilidad = nuevaDisponibilidad.IdDisponibilidad,
            DiaSemana = nuevaDisponibilidad.DiaSemana,
            HoraInicio = nuevaDisponibilidad.HoraInicio,
            HoraFin = nuevaDisponibilidad.HoraFin,
            IdMedico = nuevaDisponibilidad.IdMedico
        };

        return CreatedAtAction(nameof(GetById), new { id = disponibilidadDto.IdDisponibilidad }, disponibilidadDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateDisponibilidadDto request)
    {
        var disponibilidad = await _context.Disponibilidades.FindAsync(id);
        if (disponibilidad is null)
            return NotFound();

        var medicoExiste = await _context.Medicos.AnyAsync(m => m.IdMedico == request.IdMedico);
        if (!medicoExiste)
            return BadRequest("El médico especificado no existe.");

        disponibilidad.DiaSemana = request.DiaSemana;
        disponibilidad.HoraInicio = request.HoraInicio;
        disponibilidad.HoraFin = request.HoraFin;
        disponibilidad.IdMedico = request.IdMedico;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var disponibilidad = await _context.Disponibilidades.FindAsync(id);
        if (disponibilidad is null)
            return NotFound();

        _context.Disponibilidades.Remove(disponibilidad);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}