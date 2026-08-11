using Api.Security;
using Application.DTOs.Permiso;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class PermisosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PermisosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var permisos = await _context.Permisos
            .Select(p => new CreatePermisoDto
            {
                Codigo = p.Codigo,
                Modulo = p.Modulo,
                Descripcion = p.Descripcion
            })
            .ToListAsync();

        return Ok(permisos);
    }

    [HttpGet("modulo/{modulo}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByModulo(string modulo)
    {
        var permisos = await _context.Permisos
            .Where(p => p.Modulo.ToLower() == modulo.ToLower())
            .Select(p => new CreatePermisoDto
            {
                Codigo = p.Codigo,
                Modulo = p.Modulo,
                Descripcion = p.Descripcion
            })
            .ToListAsync();

        return Ok(permisos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var permiso = await _context.Permisos
            .Where(p => p.Id == id)
            .Select(p => new CreatePermisoDto
            {
                Codigo = p.Codigo,
                Modulo = p.Modulo,
                Descripcion = p.Descripcion
            })
            .FirstOrDefaultAsync();

        if (permiso is null)
            return NotFound();

        return Ok(permiso);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermisoDto request)
    {
        var codigoExiste = await _context.Permisos.AnyAsync(p => p.Codigo.ToLower() == request.Codigo.ToLower());
        if (codigoExiste)
            return BadRequest("El código de permiso especificado ya se encuentra registrado.");

        var nuevoPermiso = new PermisoEntity(
            codigo: request.Codigo,
            modulo: request.Modulo,
            descripcion: request.Descripcion
        );

        _context.Permisos.Add(nuevoPermiso);
        await _context.SaveChangesAsync();

        var permisoDto = new CreatePermisoDto
        {
            Codigo = nuevoPermiso.Codigo,
            Modulo = nuevoPermiso.Modulo,
            Descripcion = nuevoPermiso.Descripcion
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoPermiso.Id }, permisoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePermisoDto request)
    {
        var permiso = await _context.Permisos.FindAsync(id);
        if (permiso is null)
            return NotFound();

        var codigoEnUso = await _context.Permisos.AnyAsync(p => p.Codigo.ToLower() == request.Codigo.ToLower() && p.Id != id);
        if (codigoEnUso)
            return BadRequest("El código de permiso especificado ya se encuentra en uso por otro registro.");

        permiso.Update(
            codigo: request.Codigo,
            modulo: request.Modulo,
            descripcion: request.Descripcion
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var permiso = await _context.Permisos.FindAsync(id);
        if (permiso is null)
            return NotFound();

        _context.Permisos.Remove(permiso);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}