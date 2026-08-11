using Api.Security;
using Application.DTOs.TipoDocumento;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class TiposDocumentoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TiposDocumentoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var tiposDocumento = await _context.TiposDocumento
            .Select(td => new CreateTipoDocumentoDto
            {
                Codigo = td.Codigo,
                Nombre = td.Nombre
            })
            .ToListAsync();

        return Ok(tiposDocumento);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tipoDocumento = await _context.TiposDocumento
            .Where(td => td.Id == id)
            .Select(td => new CreateTipoDocumentoDto
            {
                Codigo = td.Codigo,
                Nombre = td.Nombre
            })
            .FirstOrDefaultAsync();

        if (tipoDocumento is null)
            return NotFound();

        return Ok(tipoDocumento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTipoDocumentoDto request)
    {
        var nuevoTipoDocumento = new TipoDocumentoEntity(
            codigo: request.Codigo,
            nombre: request.Nombre
        );

        _context.TiposDocumento.Add(nuevoTipoDocumento);
        await _context.SaveChangesAsync();

        var tipoDocumentoDto = new CreateTipoDocumentoDto
        {
            Codigo = nuevoTipoDocumento.Codigo,
            Nombre = nuevoTipoDocumento.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoTipoDocumento.Id }, tipoDocumentoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTipoDocumentoDto request)
    {
        var tipoDocumento = await _context.TiposDocumento.FindAsync(id);
        if (tipoDocumento is null)
            return NotFound();

        tipoDocumento.Update(
            codigo: request.Codigo,
            nombre: request.Nombre
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tipoDocumento = await _context.TiposDocumento.FindAsync(id);
        if (tipoDocumento is null)
            return NotFound();

        _context.TiposDocumento.Remove(tipoDocumento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}