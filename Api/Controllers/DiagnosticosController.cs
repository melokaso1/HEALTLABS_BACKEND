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
    private readonly ApplicationDbContext _context;

    public DiagnosticosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)] 
    public async Task<IActionResult> GetAll()
    {
        var diagnosticos = await _context.Diagnosticos
            .Select(d => new DiagnosticoDto
            {
                IdDiagnostico = d.IdDiagnostico,
                Nombre = d.Nombre,
                Codigo = d.Codigo
            })
            .ToListAsync();

        return Ok(diagnosticos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var diagnostico = await _context.Diagnosticos
            .Where(d => d.IdDiagnostico == id)
            .Select(d => new DiagnosticoDto
            {
                IdDiagnostico = d.IdDiagnostico,
                Nombre = d.Nombre,
                Codigo = d.Codigo
            })
            .FirstOrDefaultAsync();

        if (diagnostico is null)
            return NotFound();

        return Ok(diagnostico);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiagnosticoDto request)
    {
        var nuevoDiagnostico = new Diagnostico
        {
            IdDiagnostico = Guid.NewGuid(),
            Nombre = request.Nombre,
            Codigo = request.Codigo
        };

        _context.Diagnosticos.Add(nuevoDiagnostico);
        await _context.SaveChangesAsync();

        var diagnosticoDto = new DiagnosticoDto
        {
            IdDiagnostico = nuevoDiagnostico.IdDiagnostico,
            Nombre = nuevoDiagnostico.Nombre,
            Codigo = nuevoDiagnostico.Codigo
        };

        return CreatedAtAction(nameof(GetById), new { id = diagnosticoDto.IdDiagnostico }, diagnosticoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateDiagnosticoDto request)
    {
        var diagnostico = await _context.Diagnosticos.FindAsync(id);
        if (diagnostico is null)
            return NotFound();

        diagnostico.Nombre = request.Nombre;
        diagnostico.Codigo = request.Codigo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var diagnostico = await _context.Diagnosticos.FindAsync(id);
        if (diagnostico is null)
            return NotFound();

        _context.Diagnosticos.Remove(diagnostico);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}