using Api.Security;
using Application.DTOs.Diagnostico;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class DiagnosticosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DiagnosticosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var diagnosticos = await _context.Diagnosticos
            .Select(d => new CreateDiagnosticoDto
            {
                CodigoCie10 = d.CodigoCie10,
                Descripcion = d.Descripcion,
                Activo = d.Activo
            })
            .ToListAsync();

        return Ok(diagnosticos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var diagnostico = await _context.Diagnosticos
            .Where(d => d.Id == id)
            .Select(d => new CreateDiagnosticoDto
            {
                CodigoCie10 = d.CodigoCie10,
                Descripcion = d.Descripcion,
                Activo = d.Activo
            })
            .FirstOrDefaultAsync();

        if (diagnostico is null)
            return NotFound();

        return Ok(diagnostico);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiagnosticoDto request)
    {
        var nuevoDiagnostico = new DiagnosticoEntity(
            codigoCie10: request.CodigoCie10,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        _context.Diagnosticos.Add(nuevoDiagnostico);
        await _context.SaveChangesAsync();

        var diagnosticoDto = new CreateDiagnosticoDto
        {
            CodigoCie10 = nuevoDiagnostico.CodigoCie10,
            Descripcion = nuevoDiagnostico.Descripcion,
            Activo = nuevoDiagnostico.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoDiagnostico.Id }, diagnosticoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDiagnosticoDto request)
    {
        var diagnostico = await _context.Diagnosticos.FindAsync(id);
        if (diagnostico is null)
            return NotFound();

        diagnostico.Update(
            codigoCie10: request.CodigoCie10,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var diagnostico = await _context.Diagnosticos.FindAsync(id);
        if (diagnostico is null)
            return NotFound();

        diagnostico.Update(
            codigoCie10: diagnostico.CodigoCie10,
            descripcion: diagnostico.Descripcion,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}