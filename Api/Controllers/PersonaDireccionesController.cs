using Api.Security;
using Application.DTOs.PersonaDireccion;
using Application.UseCases.PersonaDireccion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PersonaDireccionesController : ControllerBase
{
    private readonly GetAllPersonaDireccionUseCase _getAll;
    private readonly GetPersonaDireccionByIdUseCase _getById;
    private readonly GetPersonaDireccionByPersonId _getByPersonaId;
    private readonly CreatePersonaDireccionUseCase _create;
    private readonly UpdatePersonaDireccionUseCase _update;
    private readonly DeletePersonaDireccionUseCase _delete;

    public PersonaDireccionesController(
        GetAllPersonaDireccionUseCase getAll,
        GetPersonaDireccionByIdUseCase getById,
        GetPersonaDireccionByPersonId getByPersonaId,
        CreatePersonaDireccionUseCase create,
        UpdatePersonaDireccionUseCase update,
        DeletePersonaDireccionUseCase delete)
        => (_getAll, _getById, _getByPersonaId, _create, _update, _delete)
            = (getAll, getById, getByPersonaId, create, update, delete);

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("persona/{personaId:guid}")]
    public async Task<IActionResult> GetByPersonaId(Guid personaId) =>
        Ok(await _getByPersonaId.ExecuteAsync(personaId));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaDireccionDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaDireccionDto request)
    {
        try
        {
            await _update.ExecuteAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:guid}")]
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
