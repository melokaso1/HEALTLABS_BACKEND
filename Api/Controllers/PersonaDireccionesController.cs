using Api.Security;
using Application.DTOs.PersonaDireccion;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PersonaDireccionesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonaDireccionesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var direcciones = await _context.PersonasDireccion
            .Include(pd => pd.Persona)
            .Select(pd => new CreatePersonaDireccionDto
            {
                PersonaId = pd.PersonaId,
                Direccion = pd.Direccion,
                Ciudad = pd.Ciudad,
                Tipo = pd.Tipo,
                Principal = pd.Principal
            })
            .ToListAsync();

        return Ok(direcciones);
    }

    [HttpGet("persona/{personaId:guid}")]
    public async Task<IActionResult> GetByPersona(Guid personaId)
    {
        var direcciones = await _context.PersonasDireccion
            .Where(pd => pd.PersonaId == personaId)
            .Select(pd => new CreatePersonaDireccionDto
            {
                PersonaId = pd.PersonaId,
                Direccion = pd.Direccion,
                Ciudad = pd.Ciudad,
                Tipo = pd.Tipo,
                Principal = pd.Principal
            })
            .ToListAsync();

        return Ok(direcciones);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var direccion = await _context.PersonasDireccion
            .Where(pd => pd.Id == id)
            .Select(pd => new CreatePersonaDireccionDto
            {
                PersonaId = pd.PersonaId,
                Direccion = pd.Direccion,
                Ciudad = pd.Ciudad,
                Tipo = pd.Tipo,
                Principal = pd.Principal
            })
            .FirstOrDefaultAsync();

        if (direccion is null)
            return NotFound();

        return Ok(direccion);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaDireccionDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        if (request.Principal)
        {
            var otrasPrincipales = await _context.PersonasDireccion
                .Where(pd => pd.PersonaId == request.PersonaId && pd.Principal)
                .ToListAsync();

            foreach (var item in otrasPrincipales)
            {
                item.Update(item.PersonaId, item.Direccion, item.Ciudad, item.Tipo, false);
            }
        }

        var nuevaDireccion = new PersonaDireccionEntity(
            personaId: request.PersonaId,
            direccion: request.Direccion,
            ciudad: request.Ciudad,
            tipo: request.Tipo,
            principal: request.Principal
        );

        _context.PersonasDireccion.Add(nuevaDireccion);
        await _context.SaveChangesAsync();

        var direccionDto = new CreatePersonaDireccionDto
        {
            PersonaId = nuevaDireccion.PersonaId,
            Direccion = nuevaDireccion.Direccion,
            Ciudad = nuevaDireccion.Ciudad,
            Tipo = nuevaDireccion.Tipo,
            Principal = nuevaDireccion.Principal
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaDireccion.Id }, direccionDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaDireccionDto request)
    {
        var direccion = await _context.PersonasDireccion.FindAsync(id);
        if (direccion is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        if (request.Principal)
        {
            var otrasPrincipales = await _context.PersonasDireccion
                .Where(pd => pd.PersonaId == request.PersonaId && pd.Principal && pd.Id != id)
                .ToListAsync();

            foreach (var item in otrasPrincipales)
            {
                item.Update(item.PersonaId, item.Direccion, item.Ciudad, item.Tipo, false);
            }
        }

        direccion.Update(
            personaId: request.PersonaId,
            direccion: request.Direccion,
            ciudad: request.Ciudad,
            tipo: request.Tipo,
            principal: request.Principal
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var direccion = await _context.PersonasDireccion.FindAsync(id);
        if (direccion is null)
            return NotFound();

        _context.PersonasDireccion.Remove(direccion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}