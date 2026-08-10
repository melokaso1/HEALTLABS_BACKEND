using Api.Security;
using Application.DTOs.EstadoCita;
using Domain.Entities;
using Infrastructure.Persistence.Context; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)] 
public sealed class EstadosCitaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EstadosCitaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)] 
    public async Task<IActionResult> GetAll()
    {
        var estados = await _context.EstadosCita
            .Select(e => new EstadoCitaDto
            {
                IdEstadoCita = e.IdEstadoCita,
                Nombre = e.Nombre
            })
            .ToListAsync();

        return Ok(estados);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var estado = await _context.EstadosCita
            .Where(e => e.IdEstadoCita == id)
            .Select(e => new EstadoCitaDto
            {
                IdEstadoCita = e.IdEstadoCita,
                Nombre = e.Nombre
            })
            .FirstOrDefaultAsync();

        if (estado is null)
            return NotFound();

        return Ok(estado);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstadoCitaDto request)
    {
        var nuevoEstado = new EstadoCita
        {
            IdEstadoCita = Guid.NewGuid(),
            Nombre = request.Nombre
        };

        _context.EstadosCita.Add(nuevoEstado);
        await _context.SaveChangesAsync();

        var estadoDto = new EstadoCitaDto
        {
            IdEstadoCita = nuevoEstado.IdEstadoCita,
            Nombre = nuevoEstado.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = estadoDto.IdEstadoCita }, estadoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateEstadoCitaDto request)
    {
        var estado = await _context.EstadosCita.FindAsync(id);
        if (estado is null)
            return NotFound();

        estado.Nombre = request.Nombre;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var estado = await _context.EstadosCita.FindAsync(id);
        if (estado is null)
            return NotFound();

        _context.EstadosCita.Remove(estado);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}