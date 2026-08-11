using Api.Security;
using Application.DTOs.Rol;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class RolesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RolesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _context.Roles
            .Select(r => new CreateRolDto
            {
                NombreRol = r.NombreRol,
                Descripcion = r.Descripcion,
                Activo = r.Activo
            })
            .ToListAsync();

        return Ok(roles);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rol = await _context.Roles
            .Where(r => r.Id == id)
            .Select(r => new CreateRolDto
            {
                NombreRol = r.NombreRol,
                Descripcion = r.Descripcion,
                Activo = r.Activo
            })
            .FirstOrDefaultAsync();

        if (rol is null)
            return NotFound();

        return Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolDto request)
    {
        var nuevoRol = new RolEntity(
            nombreRol: request.NombreRol,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        _context.Roles.Add(nuevoRol);
        await _context.SaveChangesAsync();

        var rolDto = new CreateRolDto
        {
            NombreRol = nuevoRol.NombreRol,
            Descripcion = nuevoRol.Descripcion,
            Activo = nuevoRol.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoRol.Id }, rolDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRolDto request)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol is null)
            return NotFound();

        rol.Update(
            nombreRol: request.NombreRol,
            descripcion: request.Descripcion,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol is null)
            return NotFound();

        rol.Update(
            nombreRol: rol.NombreRol,
            descripcion: rol.Descripcion,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}