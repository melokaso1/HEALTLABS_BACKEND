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
    private readonly AppDbContext _context;

    public PersonasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var personas = await _context.Personas
            .Include(p => p.TipoDocumento)
            .Select(p => new CreatePersonaDto
            {
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                TipoDocumentoId = p.TipoDocumentoId,
                NumeroDocumento = p.NumeroDocumento,
                FechaNacimiento = p.FechaNacimiento,
                SexoId = p.SexoId
            })
            .ToListAsync();

        return Ok(personas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var persona = await _context.Personas
            .Where(p => p.Id == id)
            .Select(p => new CreatePersonaDto
            {
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                TipoDocumentoId = p.TipoDocumentoId,
                NumeroDocumento = p.NumeroDocumento,
                FechaNacimiento = p.FechaNacimiento,
                SexoId = p.SexoId
            })
            .FirstOrDefaultAsync();

        if (persona is null)
            return NotFound();

        return Ok(persona);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaDto request)
    {
        var tipoDocExiste = await _context.TiposDocumento.AnyAsync(td => td.Id == request.TipoDocumentoId);
        if (!tipoDocExiste)
            return BadRequest("El tipo de documento especificado no existe.");

        if (request.SexoId.HasValue)
        {
            var sexoExiste = await _context.Sexos.AnyAsync(s => s.Id == request.SexoId.Value);
            if (!sexoExiste)
                return BadRequest("El sexo especificado no existe.");
        }

        var nuevaPersona = new PersonaEntity(
            nombre: request.Nombre,
            apellido: request.Apellido,
            tipoDocumentoId: request.TipoDocumentoId,
            numeroDocumento: request.NumeroDocumento,
            fechaNacimiento: request.FechaNacimiento,
            sexoId: request.SexoId
        );

        _context.Personas.Add(nuevaPersona);
        await _context.SaveChangesAsync();

        var personaDto = new CreatePersonaDto
        {
            Nombre = nuevaPersona.Nombre,
            Apellido = nuevaPersona.Apellido,
            TipoDocumentoId = nuevaPersona.TipoDocumentoId,
            NumeroDocumento = nuevaPersona.NumeroDocumento,
            FechaNacimiento = nuevaPersona.FechaNacimiento,
            SexoId = nuevaPersona.SexoId
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevaPersona.Id }, personaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaDto request)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona is null)
            return NotFound();

        var tipoDocExiste = await _context.TiposDocumento.AnyAsync(td => td.Id == request.TipoDocumentoId);
        if (!tipoDocExiste)
            return BadRequest("El tipo de documento especificado no existe.");

        if (request.SexoId.HasValue)
        {
            var sexoExiste = await _context.Sexos.AnyAsync(s => s.Id == request.SexoId.Value);
            if (!sexoExiste)
                return BadRequest("El sexo especificado no existe.");
        }

        persona.Update(
            nombre: request.Nombre,
            apellido: request.Apellido,
            tipoDocumentoId: request.TipoDocumentoId,
            numeroDocumento: request.NumeroDocumento,
            fechaNacimiento: request.FechaNacimiento,
            sexoId: request.SexoId
        );

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