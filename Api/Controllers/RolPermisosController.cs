using Api.Security;
using Application.DTOs.RolPermiso;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class RolPermisosController : ControllerBase
{
    private readonly AppDbContext _context;

    public RolPermisosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/RolPermisos
    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var rolPermisos = await _context.RolesPermiso
            .Include(rp => rp.Rol)
            .Include(rp => rp.Permiso)
            .Select(rp => new CreateRolPermisoDto
            {
                RolId = rp.RolId,
                PermisoId = rp.PermisoId
            })
            .ToListAsync();

        return Ok(rolPermisos);
    }

    // GET: api/RolPermisos/rol/{rolId}
    [HttpGet("rol/{rolId:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetByRol(Guid rolId)
    {
        var rolPermisos = await _context.RolesPermiso
            .Include(rp => rp.Permiso)
            .Where(rp => rp.RolId == rolId)
            .Select(rp => new CreateRolPermisoDto
            {
                RolId = rp.RolId,
                PermisoId = rp.PermisoId
            })
            .ToListAsync();

        return Ok(rolPermisos);
    }

    // GET: api/RolPermisos/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rolPermiso = await _context.RolesPermiso
            .Where(rp => rp.Id == id)
            .Select(rp => new CreateRolPermisoDto
            {
                RolId = rp.RolId,
                PermisoId = rp.PermisoId
            })
            .FirstOrDefaultAsync();

        if (rolPermiso is null)
            return NotFound();

        return Ok(rolPermiso);
    }

    // POST: api/RolPermisos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolPermisoDto request)
    {
        var rolExiste = await _context.Roles.AnyAsync(r => r.Id == request.RolId && r.Activo);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe o se encuentra inactivo.");

        var permisoExiste = await _context.Permisos.AnyAsync(p => p.Id == request.PermisoId);
        if (!permisoExiste)
            return BadRequest("El permiso especificado no existe.");

        var yaAsignado = await _context.RolesPermiso
            .AnyAsync(rp => rp.RolId == request.RolId && rp.PermisoId == request.PermisoId);
        if (yaAsignado)
            return BadRequest("Este permiso ya se encuentra asignado al rol especificado.");

        var nuevoRolPermiso = new RolPermisoEntity(
            rolId: request.RolId,
            permisoId: request.PermisoId
        );

        _context.RolesPermiso.Add(nuevoRolPermiso);
        await _context.SaveChangesAsync();

        var rolPermisoDto = new CreateRolPermisoDto
        {
            RolId = nuevoRolPermiso.RolId,
            PermisoId = nuevoRolPermiso.PermisoId
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoRolPermiso.Id }, rolPermisoDto);
    }

    // PUT: api/RolPermisos/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateRolPermisoDto request)
    {
        var rolPermiso = await _context.RolesPermiso.FindAsync(id);
        if (rolPermiso is null)
            return NotFound();

        var rolExiste = await _context.Roles.AnyAsync(r => r.Id == request.RolId && r.Activo);
        if (!rolExiste)
            return BadRequest("El rol especificado no existe o se encuentra inactivo.");

        var permisoExiste = await _context.Permisos.AnyAsync(p => p.Id == request.PermisoId);
        if (!permisoExiste)
            return BadRequest("El permiso especificado no existe.");

        var yaAsignado = await _context.RolesPermiso
            .AnyAsync(rp => rp.RolId == request.RolId && rp.PermisoId == request.PermisoId && rp.Id != id);
        if (yaAsignado)
            return BadRequest("Esta combinación de rol y permiso ya existe en otro registro.");

        rolPermiso.Update(
            rolId: request.RolId,
            permisoId: request.PermisoId
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/RolPermisos/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rolPermiso = await _context.RolesPermiso.FindAsync(id);
        if (rolPermiso is null)
            return NotFound();

        _context.RolesPermiso.Remove(rolPermiso);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}