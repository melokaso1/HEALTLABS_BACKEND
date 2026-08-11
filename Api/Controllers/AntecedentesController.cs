using Api.Security;
using Application.DTOs.Antecedente;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class AntecedentesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AntecedentesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var antecedentes = await _context.Antecedentes
            .Include(a => a.Paciente)
            .Include(a => a.UsuarioRegistro)
            .Select(a => new CreateAntecedenteDto
            {
                PacienteId = a.PacienteId,
                Tipo = a.Tipo,
                Descripcion = a.Descripcion,
                UsuarioRegistroId = a.UsuarioRegistroId
            })
            .ToListAsync();

        return Ok(antecedentes);
    }

    [HttpGet("paciente/{pacienteId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByPaciente(Guid pacienteId)
    {
        var antecedentes = await _context.Antecedentes
            .Where(a => a.PacienteId == pacienteId)
            .Select(a => new CreateAntecedenteDto
            {
                PacienteId = a.PacienteId,
                Tipo = a.Tipo,
                Descripcion = a.Descripcion,
                UsuarioRegistroId = a.UsuarioRegistroId
            })
            .ToListAsync();

        return Ok(antecedentes);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var antecedente = await _context.Antecedentes
            .Where(a => a.Id == id)
            .Select(a => new CreateAntecedenteDto
            {
                PacienteId = a.PacienteId,
                Tipo = a.Tipo,
                Descripcion = a.Descripcion,
                UsuarioRegistroId = a.UsuarioRegistroId
            })
            .FirstOrDefaultAsync();

        if (antecedente is null)
            return NotFound();

        return Ok(antecedente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAntecedenteDto request)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId);
        if (!pacienteExiste)
            return BadRequest("El paciente especificado no existe.");

        if (request.UsuarioRegistroId.HasValue)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioRegistroId.Value);
            if (!usuarioExiste)
                return BadRequest("El usuario de registro especificado no existe.");
        }

        var nuevoAntecedente = new AntecedenteEntity(
            pacienteId: request.PacienteId,
            tipo: request.Tipo,
            descripcion: request.Descripcion,
            usuarioRegistroId: request.UsuarioRegistroId
        );

        _context.Antecedentes.Add(nuevoAntecedente);
        await _context.SaveChangesAsync();

        var antecedenteDto = new CreateAntecedenteDto
        {
            PacienteId = nuevoAntecedente.PacienteId,
            Tipo = nuevoAntecedente.Tipo,
            Descripcion = nuevoAntecedente.Descripcion,
            UsuarioRegistroId = nuevoAntecedente.UsuarioRegistroId
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoAntecedente.Id }, antecedenteDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAntecedenteDto request)
    {
        var antecedente = await _context.Antecedentes.FindAsync(id);
        if (antecedente is null)
            return NotFound();

        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == request.PacienteId);
        if (!pacienteExiste)
            return BadRequest("El paciente especificado no existe.");

        if (request.UsuarioRegistroId.HasValue)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioRegistroId.Value);
            if (!usuarioExiste)
                return BadRequest("El usuario de registro especificado no existe.");
        }

        antecedente.Update(
            pacienteId: request.PacienteId,
            tipo: request.Tipo,
            descripcion: request.Descripcion,
            usuarioRegistroId: request.UsuarioRegistroId
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var antecedente = await _context.Antecedentes.FindAsync(id);
        if (antecedente is null)
            return NotFound();

        _context.Antecedentes.Remove(antecedente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}