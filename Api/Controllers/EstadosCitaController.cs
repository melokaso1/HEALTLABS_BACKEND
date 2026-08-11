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
    private readonly AppDbContext _context;

    public EstadosCitaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var estados = await _context.EstadosCita
            .Select(e => new CreateEstadoCitaDto
            {
                Codigo = e.Codigo,
                Descripcion = e.Descripcion
            })
            .ToListAsync();

        return Ok(estados);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var estado = await _context.EstadosCita
            .Where(e => e.Id == id)
            .Select(e => new CreateEstadoCitaDto
            {
                Codigo = e.Codigo,
                Descripcion = e.Descripcion
            })
            .FirstOrDefaultAsync();

        if (estado is null)
            return NotFound();

        return Ok(estado);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstadoCitaDto request)
    {
        var codigoExiste = await _context.EstadosCita.AnyAsync(e => e.Codigo == request.Codigo);
        if (codigoExiste)
            return BadRequest("El código de estado ya se encuentra registrado.");

        var nuevoEstado = new EstadoCitaEntity(
            codigo: request.Codigo,
            descripcion: request.Descripcion
        );

        _context.EstadosCita.Add(nuevoEstado);
        await _context.SaveChangesAsync();

        var estadoDto = new CreateEstadoCitaDto
        {
            Codigo = nuevoEstado.Codigo,
            Descripcion = nuevoEstado.Descripcion
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoEstado.Id }, estadoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEstadoCitaDto request)
    {
        var estado = await _context.EstadosCita.FindAsync(id);
        if (estado is null)
            return NotFound();

        var codigoExiste = await _context.EstadosCita.AnyAsync(e => e.Codigo == request.Codigo && e.Id != id);
        if (codigoExiste)
            return BadRequest("El código de estado ya está en uso por otro registro.");

        estado.Update(
            codigo: request.Codigo,
            descripcion: request.Descripcion
        );

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