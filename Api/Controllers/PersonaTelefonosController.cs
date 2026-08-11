using Api.Security;
using Application.DTOs.PersonaTelefono;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PersonaTelefonosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonaTelefonosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var telefonos = await _context.PersonasTelefono
            .Include(pt => pt.Persona)
            .Select(pt => new CreatePersonaTelefonoDto
            {
                PersonaId = pt.PersonaId,
                Telefono = pt.Telefono,
                Tipo = pt.Tipo,
                Principal = pt.Principal
            })
            .ToListAsync();

        return Ok(telefonos);
    }

    [HttpGet("persona/{personaId:guid}")]
    public async Task<IActionResult> GetByPersona(Guid personaId)
    {
        var telefonos = await _context.PersonasTelefono
            .Where(pt => pt.PersonaId == personaId)
            .Select(pt => new CreatePersonaTelefonoDto
            {
                PersonaId = pt.PersonaId,
                Telefono = pt.Telefono,
                Tipo = pt.Tipo,
                Principal = pt.Principal
            })
            .ToListAsync();

        return Ok(telefonos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var telefono = await _context.PersonasTelefono
            .Where(pt => pt.Id == id)
            .Select(pt => new CreatePersonaTelefonoDto
            {
                PersonaId = pt.PersonaId,
                Telefono = pt.Telefono,
                Tipo = pt.Tipo,
                Principal = pt.Principal
            })
            .FirstOrDefaultAsync();

        if (telefono is null)
            return NotFound();

        return Ok(telefono);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaTelefonoDto request)
    {
        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var telefonoExiste = await _context.PersonasTelefono
            .AnyAsync(pt => pt.PersonaId == request.PersonaId && pt.Telefono == request.Telefono);
        if (telefonoExiste)
            return BadRequest("Este número de teléfono ya está registrado para esta persona.");

        if (request.Principal)
        {
            var otrosPrincipales = await _context.PersonasTelefono
                .Where(pt => pt.PersonaId == request.PersonaId && pt.Principal)
                .ToListAsync();

            foreach (var item in otrosPrincipales)
            {
                item.Update(item.PersonaId, item.Telefono, item.Tipo, false);
            }
        }

        var nuevoTelefono = new PersonaTelefonoEntity(
            personaId: request.PersonaId,
            telefono: request.Telefono,
            tipo: request.Tipo,
            principal: request.Principal
        );

        _context.PersonasTelefono.Add(nuevoTelefono);
        await _context.SaveChangesAsync();

        var telefonoDto = new CreatePersonaTelefonoDto
        {
            PersonaId = nuevoTelefono.PersonaId,
            Telefono = nuevoTelefono.Telefono,
            Tipo = nuevoTelefono.Tipo,
            Principal = nuevoTelefono.Principal
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoTelefono.Id }, telefonoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaTelefonoDto request)
    {
        var telefono = await _context.PersonasTelefono.FindAsync(id);
        if (telefono is null)
            return NotFound();

        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == request.PersonaId);
        if (!personaExiste)
            return BadRequest("La persona especificada no existe.");

        var telefonoDuplicado = await _context.PersonasTelefono
            .AnyAsync(pt => pt.PersonaId == request.PersonaId && pt.Telefono == request.Telefono && pt.Id != id);
        if (telefonoDuplicado)
            return BadRequest("Este número de teléfono ya se encuentra registrado en otro registro para esta persona.");

        if (request.Principal)
        {
            var otrosPrincipales = await _context.PersonasTelefono
                .Where(pt => pt.PersonaId == request.PersonaId && pt.Principal && pt.Id != id)
                .ToListAsync();

            foreach (var item in otrosPrincipales)
            {
                item.Update(item.PersonaId, item.Telefono, item.Tipo, false);
            }
        }

        telefono.Update(
            personaId: request.PersonaId,
            telefono: request.Telefono,
            tipo: request.Tipo,
            principal: request.Principal
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var telefono = await _context.PersonasTelefono.FindAsync(id);
        if (telefono is null)
            return NotFound();

        _context.PersonasTelefono.Remove(telefono);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}