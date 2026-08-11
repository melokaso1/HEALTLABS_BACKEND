using Api.Security;
using Application.DTOs.TipoCita;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Admin)]
public sealed class TiposCitaController : ControllerBase
{
    private readonly AppDbContext _context;

    public TiposCitaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll()
    {
        var tiposCita = await _context.TiposCita
            .Select(tc => new CreateTipoCitaDto
            {
                Codigo = tc.Codigo,
                Nombre = tc.Nombre,
                DuracionMinutos = tc.DuracionMinutos,
                Activo = tc.Activo
            })
            .ToListAsync();

        return Ok(tiposCita);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tipoCita = await _context.TiposCita
            .Where(tc => tc.Id == id)
            .Select(tc => new CreateTipoCitaDto
            {
                Codigo = tc.Codigo,
                Nombre = tc.Nombre,
                DuracionMinutos = tc.DuracionMinutos,
                Activo = tc.Activo
            })
            .FirstOrDefaultAsync();

        if (tipoCita is null)
            return NotFound();

        return Ok(tipoCita);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTipoCitaDto request)
    {
        if (request.DuracionMinutos <= 0)
            return BadRequest("La duración en minutos debe ser mayor a 0.");

        var codigoExiste = await _context.TiposCita.AnyAsync(tc => tc.Codigo == request.Codigo);
        if (codigoExiste)
            return BadRequest("El código de tipo de cita ya se encuentra registrado.");

        var nuevoTipoCita = new TipoCitaEntity(
            codigo: request.Codigo,
            nombre: request.Nombre,
            duracionMinutos: request.DuracionMinutos,
            activo: request.Activo
        );

        _context.TiposCita.Add(nuevoTipoCita);
        await _context.SaveChangesAsync();

        var tipoCitaDto = new CreateTipoCitaDto
        {
            Codigo = nuevoTipoCita.Codigo,
            Nombre = nuevoTipoCita.Nombre,
            DuracionMinutos = nuevoTipoCita.DuracionMinutos,
            Activo = nuevoTipoCita.Activo
        };

        return CreatedAtAction(nameof(GetById), new { id = nuevoTipoCita.Id }, tipoCitaDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTipoCitaDto request)
    {
        var tipoCita = await _context.TiposCita.FindAsync(id);
        if (tipoCita is null)
            return NotFound();

        if (request.DuracionMinutos <= 0)
            return BadRequest("La duración en minutos debe ser mayor a 0.");

        var codigoEnUso = await _context.TiposCita.AnyAsync(tc => tc.Codigo == request.Codigo && tc.Id != id);
        if (codigoEnUso)
            return BadRequest("El código de tipo de cita ya se encuentra en uso por otro registro.");

        tipoCita.Update(
            codigo: request.Codigo,
            nombre: request.Nombre,
            duracionMinutos: request.DuracionMinutos,
            activo: request.Activo
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tipoCita = await _context.TiposCita.FindAsync(id);
        if (tipoCita is null)
            return NotFound();

        tipoCita.Update(
            codigo: tipoCita.Codigo,
            nombre: tipoCita.Nombre,
            duracionMinutos: tipoCita.DuracionMinutos,
            activo: false
        );

        await _context.SaveChangesAsync();
        return NoContent();
    }
}