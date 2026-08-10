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
    private readonly ApplicationDbContext _context;

    public CargosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cargos = await _context.Cargos
            .Select(c => new CargoDto
            {
                IdCargo = c.IdCargo,
                Nombre = c.Nombre
            })
            .ToListAsync();

        return Ok(cargos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cargo = await _context.Cargos
            .Where(c => c.IdCargo == id)
            .Select(c => new CargoDto
            {
                IdCargo = c.IdCargo,
                Nombre = c.Nombre
            })
            .FirstOrDefaultAsync();

        if (cargo is null)
            return NotFound();

        return Ok(cargo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCargoDto request)
    {
        var nuevoCargo = new Cargo
        {
            IdCargo = Guid.NewGuid(),
            Nombre = request.Nombre
        };

        _context.Cargos.Add(nuevoCargo);
        await _context.SaveChangesAsync();

        var cargoDto = new CargoDto
        {
            IdCargo = nuevoCargo.IdCargo,
            Nombre = nuevoCargo.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = cargoDto.IdCargo }, cargoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateCargoDto request)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo is null)
            return NotFound();

        cargo.Nombre = request.Nombre;

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