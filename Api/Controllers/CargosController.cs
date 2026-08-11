using Api.Security;
using Application.DTOs.Cargo;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class CargosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CargosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cargos = await _context.Cargos
            .Select(c => new CreateCargoDto
            {
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                NivelJerarquico = c.NivelJerarquico
            })
            .ToListAsync();

        return Ok(cargos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cargo = await _context.Cargos
            .Where(c => c.Id == id)
            .Select(c => new CreateCargoDto
            {
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                NivelJerarquico = c.NivelJerarquico
            })
            .FirstOrDefaultAsync();

        if (cargo is null)
            return NotFound();

        return Ok(cargo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCargoDto request)
    {
        var nuevoCargo = new CargoEntity(
            codigo: request.Codigo,
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            nivelJerarquico: request.NivelJerarquico
        );

        _context.Cargos.Add(nuevoCargo);
        await _context.SaveChangesAsync();

        var cargoDto = new CreateCargoDto
        {
            Codigo = nuevoCargo.Codigo,
            Nombre = nuevoCargo.Nombre,
            Descripcion = nuevoCargo.Descripcion,
            NivelJerarquico = nuevoCargo.NivelJerarquico
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoCargo.Id }, cargoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCargoDto request)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo is null)
            return NotFound();

        cargo.Update(
            codigo: request.Codigo,
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            nivelJerarquico: request.NivelJerarquico
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo is null)
            return NotFound();

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}