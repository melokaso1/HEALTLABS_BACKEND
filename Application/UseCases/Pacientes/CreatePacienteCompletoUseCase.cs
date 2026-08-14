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
        var strategy = _context.Database.CreateExecutionStrategy();
        PacienteEntity? paciente = null;

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

            var persona = new PersonaEntity(
                dto.Persona.Nombre,
                dto.Persona.Apellido,
                dto.Persona.TipoDocumentoId,
                documento,
                dto.Persona.FechaNacimiento,
                dto.Persona.SexoId);
            paciente = new PacienteEntity(persona.Id, dto.Activo, dto.TipoSangre);

            _context.Personas.Add(persona);
            _context.Pacientes.Add(paciente);

            if (!string.IsNullOrWhiteSpace(dto.Telefono))
                _context.PersonasTelefono.Add(new PersonaTelefonoEntity(persona.Id, dto.Telefono, dto.TipoTelefono, true));
            if (!string.IsNullOrWhiteSpace(dto.Direccion))
                _context.PersonasDireccion.Add(new PersonaDireccionEntity(persona.Id, dto.Direccion, dto.Ciudad, true));

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        });

        return paciente!;
    }
}
