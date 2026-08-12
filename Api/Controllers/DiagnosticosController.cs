using Api.Security;
using Application.DTOs.Diagnostico;
using Application.UseCases.Diagnostico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class DiagnosticosController : ControllerBase
{
    private readonly GetAllDiagnosticosUseCase _getAll;
    private readonly GetDiagnosticoByIdUseCase _getById;
    private readonly CreateDiagnosticoUseCase _create;
    private readonly DeleteDiagnosticoUseCase _delete;

    public DiagnosticosController(GetAllDiagnosticosUseCase getAll, GetDiagnosticoByIdUseCase getById,
        CreateDiagnosticoUseCase create, DeleteDiagnosticoUseCase delete)
        => (_getAll, _getById, _create, _delete) = (getAll, getById, create, delete);

    [HttpGet]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Todos)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateDiagnosticoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (await _getById.ExecuteAsync(id) is null) return NotFound();
            await _delete.ExecuteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}
