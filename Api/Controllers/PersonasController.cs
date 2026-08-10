using Api.Security;
using Application.DTOs.Persona;
using Domain.Entities;
using Infrastructure.Persistence.Context; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)] 
public sealed class PersonasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PersonasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var personas = await _context.Personas
            .Include(p => p.TipoDocumento)
            .Select(p => new PersonaDto
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NumeroDocumento = p.NumeroDocumento,
                Telefono = p.Telefono,
                Email = p.Email,
                IdTipoDocumento = p.IdTipoDocumento
            })
            .ToListAsync();

        return Ok(personas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var persona = await _context.Personas
            .Where(p => p.IdPersona == id)
            .Select(p => new PersonaDto
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NumeroDocumento = p.NumeroDocumento,
                Telefono = p.Telefono,
                Email = p.Email,
                IdTipoDocumento = p.IdTipoDocumento
            })
            .FirstOrDefaultAsync();

        if (persona is null)
            return NotFound();

        return Ok(persona);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaDto request)
    {
        var tipoDocExiste = await _context.TiposDocumento.AnyAsync(td => td.IdTipoDocumento == request.IdTipoDocumento);
        if (!tipoDocExiste)
            return BadRequest("El tipo de documento especificado no existe.");

        var nuevaPersona = new Persona
        {
            IdPersona = Guid.NewGuid(),
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            NumeroDocumento = request.NumeroDocumento,
            Telefono = request.Telefono,
            Email = request.Email,
            IdTipoDocumento = request.IdTipoDocumento
        };

        _context.Personas.Add(nuevaPersona);
        await _context.SaveChangesAsync();

        var personaDto = new PersonaDto
        {
            IdPersona = nuevaPersona.IdPersona,
            Nombre = nuevaPersona.Nombre,
            Apellido = nuevaPersona.Apellido,
            NumeroDocumento = nuevaPersona.NumeroDocumento,
            Telefono = nuevaPersona.Telefono,
            Email = nuevaPersona.Email,
            IdTipoDocumento = nuevaPersona.IdTipoDocumento
        };

        return CreatedAtAction(nameof(GetById), new { id = personaDto.IdPersona }, personaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreatePersonaDto request)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona is null)
            return NotFound();

        var tipoDocExiste = await _context.TiposDocumento.AnyAsync(td => td.IdTipoDocumento == request.IdTipoDocumento);
        if (!tipoDocExiste)
            return BadRequest("El tipo de documento especificado no existe.");

        persona.Nombre = request.Nombre;
        persona.Apellido = request.Apellido;
        persona.NumeroDocumento = request.NumeroDocumento;
        persona.Telefono = request.Telefono;
        persona.Email = request.Email;
        persona.IdTipoDocumento = request.IdTipoDocumento;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)] 
    public async Task<IActionResult> Delete(Guid id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona is null)
            return NotFound();

        _context.Personas.Remove(persona);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}