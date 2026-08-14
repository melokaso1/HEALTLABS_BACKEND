using Application.DTOs.Paciente;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Pacientes;

public sealed class CreatePacienteCompletoUseCase
{
    private readonly AppDbContext _context;

    public CreatePacienteCompletoUseCase(AppDbContext context) => _context = context;

    public async Task<PacienteEntity> ExecuteAsync(CreatePacienteCompletoDto dto)
    {
        var documento = NumeroDocumentoValueObject.Create(dto.Persona.NumeroDocumento).Value;
        var persona = new PersonaEntity(
            dto.Persona.Nombre,
            dto.Persona.Apellido,
            dto.Persona.TipoDocumentoId,
            documento,
            dto.Persona.FechaNacimiento,
            dto.Persona.SexoId);
        persona.Email = string.IsNullOrWhiteSpace(dto.Email)
            ? null
            : EmailValueObject.Create(dto.Email).Value;
        var paciente = new PacienteEntity(persona.Id, dto.Activo, dto.TipoSangre);
        PersonaTelefonoEntity? telefono = !string.IsNullOrWhiteSpace(dto.Telefono)
            ? new PersonaTelefonoEntity(persona.Id, dto.Telefono, dto.TipoTelefono, true)
            : null;
        PersonaDireccionEntity? direccion = !string.IsNullOrWhiteSpace(dto.Direccion)
            ? new PersonaDireccionEntity(persona.Id, dto.Direccion, dto.Ciudad, true)
            : null;

        _context.Personas.Add(persona);
        _context.Pacientes.Add(paciente);
        if (telefono is not null)
            _context.PersonasTelefono.Add(telefono);
        if (direccion is not null)
            _context.PersonasDireccion.Add(direccion);

        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            if (!await _context.TiposDocumento.AnyAsync(x => x.Id == dto.Persona.TipoDocumentoId))
                throw new InvalidOperationException("El tipo de documento especificado no existe.");
            if (dto.Persona.SexoId.HasValue && !await _context.Sexos.AnyAsync(x => x.Id == dto.Persona.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");
            if (await _context.Personas.AnyAsync(x =>
                    x.TipoDocumentoId == dto.Persona.TipoDocumentoId && x.NumeroDocumento == documento))
                throw new DuplicateDocumentException();

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        });

        return await _context.Pacientes
            .AsNoTracking()
            .Include(p => p.Persona!)
                .ThenInclude(persona => persona.TipoDocumento!)
            .Include(p => p.Persona!)
                .ThenInclude(persona => persona.Sexo!)
            .Include(p => p.Persona!)
                .ThenInclude(persona => persona.Telefonos!)
            .Include(p => p.Persona!)
                .ThenInclude(persona => persona.Direcciones!)
            .SingleAsync(p => p.Id == paciente.Id);
    }
}
