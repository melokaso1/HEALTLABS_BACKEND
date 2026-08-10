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
    private readonly ApplicationDbContext _context;

    public RolesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Roles
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _context.Roles
            .Select(r => new RolDto
            {
                IdRol = r.IdRol,
                Nombre = r.Nombre
            })
            .ToListAsync();

        return Ok(roles);
    }

    // GET: api/Roles/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rol = await _context.Roles
            .Where(r => r.IdRol == id)
            .Select(r => new RolDto
            {
                IdRol = r.IdRol,
                Nombre = r.Nombre
            })
            .FirstOrDefaultAsync();

        if (rol is null)
            return NotFound();

        return Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolDto request)
    {
        var nuevoRol = new Rol
        {
            IdRol = Guid.NewGuid(),
            Nombre = request.Nombre
        };

        _context.Roles.Add(nuevoRol);
        await _context.SaveChangesAsync();

        var rolDto = new RolDto
        {
            IdRol = nuevoRol.IdRol,
            Nombre = nuevoRol.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = rolDto.IdRol }, rolDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateRolDto request)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol is null)
            return NotFound();

        rol.Nombre = request.Nombre;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol is null)
            return NotFound();

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}