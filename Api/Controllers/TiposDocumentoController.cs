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
    private readonly ApplicationDbContext _context;

    public TiposDocumentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous] 
    public async Task<IActionResult> GetAll()
    {
        var tiposDocumento = await _context.TiposDocumento
            .Select(td => new TipoDocumentoDto
            {
                IdTipoDocumento = td.IdTipoDocumento,
                Nombre = td.Nombre
            })
            .ToListAsync();

        return Ok(tiposDocumento);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tipoDocumento = await _context.TiposDocumento
            .Where(td => td.IdTipoDocumento == id)
            .Select(td => new TipoDocumentoDto
            {
                IdTipoDocumento = td.IdTipoDocumento,
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
        var nuevoTipoDocumento = new TipoDocumento
        {
            IdTipoDocumento = Guid.NewGuid(),
            Nombre = request.Nombre
        };

        _context.TiposDocumento.Add(nuevoTipoDocumento);
        await _context.SaveChangesAsync();

        var tipoDocumentoDto = new TipoDocumentoDto
        {
            IdTipoDocumento = nuevoTipoTipoDocumento.IdTipoDocumento,
            Nombre = nuevoTipoDocumento.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = tipoDocumentoDto.IdTipoDocumento }, tipoDocumentoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateTipoDocumentoDto request)
    {
        var tipoDocumento = await _context.TiposDocumento.FindAsync(id);
        if (tipoDocumento is null)
            return NotFound();

        tipoDocumento.Nombre = request.Nombre;

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