using Application.DTOs.Medico;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Medicos;

public sealed class CreateMedicoCompletoUseCase
{
    private readonly AppDbContext _context;

    public CreateMedicoCompletoUseCase(AppDbContext context) => _context = context;

    public async Task<MedicoEntity> ExecuteAsync(CreateMedicoCompletoDto dto)
    {
        var documento = NumeroDocumentoValueObject.Create(dto.Persona.NumeroDocumento).Value;
        var strategy = _context.Database.CreateExecutionStrategy();
        MedicoEntity? medico = null;

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            if (!await _context.TiposDocumento.AnyAsync(x => x.Id == dto.Persona.TipoDocumentoId))
                throw new InvalidOperationException("El tipo de documento especificado no existe.");
            if (dto.Persona.SexoId.HasValue && !await _context.Sexos.AnyAsync(x => x.Id == dto.Persona.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");
            if (!await _context.Cargos.AnyAsync(x => x.Id == dto.CargoId))
                throw new InvalidOperationException("El cargo especificado no existe.");
            if (dto.EspecialidadId.HasValue && !await _context.Especialidades.AnyAsync(x => x.Id == dto.EspecialidadId.Value))
                throw new InvalidOperationException("La especialidad especificada no existe.");
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
            var empleado = new EmpleadoEntity(
                persona.Id,
                dto.CargoId,
                dto.FechaIngreso,
                dto.FechaRetiro,
                dto.EmpleadoActivo);
            medico = new MedicoEntity(empleado.Id, dto.RegistroProfesional, dto.MedicoActivo);

            _context.Personas.Add(persona);
            _context.Empleados.Add(empleado);
            _context.Medicos.Add(medico);
            if (dto.EspecialidadId.HasValue)
                _context.MedicosEspecialidad.Add(
                    new MedicoEspecialidadEntity(medico.Id, dto.EspecialidadId.Value, dto.EspecialidadPrincipal));

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        });

        return medico!;
    }
}
