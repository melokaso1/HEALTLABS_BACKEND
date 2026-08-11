using Api.Security;
using Application.DTOs.Sexo;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class SexosController : ControllerBase
{
    private readonly AppDbContext _context;

    public SexosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var sexos = await _context.Sexos
            .Select(s => new CreateSexoDto
            {
                Codigo = s.Codigo,
                Nombre = s.Nombre
            })
            .ToListAsync();

        return Ok(sexos);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sexo = await _context.Sexos
            .Where(s => s.Id == id)
            .Select(s => new CreateSexoDto
            {
                Codigo = s.Codigo,
                Nombre = s.Nombre
            })
            .FirstOrDefaultAsync();

        if (sexo is null)
            return NotFound();

        return Ok(sexo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSexoDto request)
    {
        var codigoExiste = await _context.Sexos.AnyAsync(s => s.Codigo == request.Codigo);
        if (codigoExiste)
            return BadRequest("El código de sexo especificado ya se encuentra registrado.");

        var nuevoSexo = new SexoEntity(
            codigo: request.Codigo,
            nombre: request.Nombre
        );

        _context.Sexos.Add(nuevoSexo);
        await _context.SaveChangesAsync();

        var sexoDto = new CreateSexoDto
        {
            Codigo = nuevoSexo.Codigo,
            Nombre = nuevoSexo.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoSexo.Id }, sexoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSexoDto request)
    {
        var sexo = await _context.Sexos.FindAsync(id);
        if (sexo is null)
            return NotFound();

        var codigoEnUso = await _context.Sexos.AnyAsync(s => s.Codigo == request.Codigo && s.Id != id);
        if (codigoEnUso)
            return BadRequest("El código de sexo especificado ya se encuentra en uso por otro registro.");

        sexo.Update(
            codigo: request.Codigo,
            nombre: request.Nombre
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sexo = await _context.Sexos.FindAsync(id);
        if (sexo is null)
            return NotFound();

        _context.Sexos.Remove(sexo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}