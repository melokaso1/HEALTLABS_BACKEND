using Api.Security;
using Application.DTOs.PersonaTelefono;
using Application.UseCases.PersonaTelefono;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class PersonaTelefonosController : ControllerBase
{
    private readonly GetAllPersonaTelefonoUseCase _getAll;
    private readonly GetPersonaTelefonoByIdUseCase _getById;
    private readonly CreatePersonaTelefonoUseCase _create;
    private readonly UpdatePersonaTelefonoUseCase _update;
    private readonly DeletePersonaTelefonoUseCase _delete;

    public PersonaTelefonosController(GetAllPersonaTelefonoUseCase getAll, GetPersonaTelefonoByIdUseCase getById,
        CreatePersonaTelefonoUseCase create, UpdatePersonaTelefonoUseCase update, DeletePersonaTelefonoUseCase delete)
        => (_getAll, _getById, _create, _update, _delete) = (getAll, getById, create, update, delete);

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _getAll.ExecuteAsync());

    [HttpGet("persona/{personaId:guid}")]
    public async Task<IActionResult> GetByPersonaId(Guid personaId) =>
        Ok((await _getAll.ExecuteAsync()).Where(entity => entity.PersonaId == personaId));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _getById.ExecuteAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaTelefonoDto request)
    {
        try
        {
            var entity = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaTelefonoDto request)
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
