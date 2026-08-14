using Application.DTOs.Paciente;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Pacientes
{
    public class UpdatePacienteUseCase
    {
        private readonly AppDbContext _context;

        public UpdatePacienteUseCase(AppDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Guid id, UpdatePacienteDto dto)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Persona!)
                    .ThenInclude(persona => persona.Telefonos!)
                .Include(p => p.Persona!)
                    .ThenInclude(persona => persona.Direcciones!)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException("No Existe");

            var persona = paciente.Persona
                ?? throw new InvalidOperationException("El paciente no tiene una persona asociada.");

            var tipoDocumentoId = dto.TipoDocumentoId ?? persona.TipoDocumentoId;
            var numeroDocumento = dto.NumeroDocumento is null
                ? persona.NumeroDocumento
                : NumeroDocumentoValueObject.Create(dto.NumeroDocumento).Value;

            if (dto.TipoDocumentoId.HasValue
                && !await _context.TiposDocumento.AnyAsync(x => x.Id == tipoDocumentoId))
            {
                throw new InvalidOperationException("El tipo de documento especificado no existe.");
            }

            if (dto.SexoId.HasValue && !await _context.Sexos.AnyAsync(x => x.Id == dto.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");

            if (tipoDocumentoId != persona.TipoDocumentoId || numeroDocumento != persona.NumeroDocumento)
            {
                var documentoDuplicado = await _context.Personas.AnyAsync(x =>
                    x.Id != persona.Id
                    && x.TipoDocumentoId == tipoDocumentoId
                    && x.NumeroDocumento == numeroDocumento);

                if (documentoDuplicado)
                    throw new DuplicateDocumentException();
            }

            if (dto.Nombre is not null || dto.Apellido is not null || dto.TipoDocumentoId.HasValue
                || dto.NumeroDocumento is not null || dto.FechaNacimiento.HasValue || dto.SexoId.HasValue)
            {
                persona.Update(
                    dto.Nombre ?? persona.Nombre,
                    dto.Apellido ?? persona.Apellido,
                    tipoDocumentoId,
                    numeroDocumento,
                    dto.FechaNacimiento ?? persona.FechaNacimiento,
                    dto.SexoId ?? persona.SexoId);
            }

            if (!string.IsNullOrWhiteSpace(dto.Telefono))
            {
                var telefono = persona.Telefonos.FirstOrDefault(t => t.Principal)
                    ?? persona.Telefonos.FirstOrDefault();

                if (telefono is null)
                    _context.PersonasTelefono.Add(new PersonaTelefonoEntity(persona.Id, dto.Telefono, dto.TipoTelefono, true));
                else
                    telefono.Update(dto.Telefono, dto.TipoTelefono ?? telefono.Tipo, true);
            }

            if (!string.IsNullOrWhiteSpace(dto.Direccion))
            {
                var direccion = persona.Direcciones.FirstOrDefault(d => d.Principal)
                    ?? persona.Direcciones.FirstOrDefault();

                if (direccion is null)
                    _context.PersonasDireccion.Add(new PersonaDireccionEntity(persona.Id, dto.Direccion, dto.Ciudad, true));
                else
                    direccion.Update(dto.Direccion, dto.Ciudad ?? direccion.Ciudad, true);
            }

            if (dto.Activo.HasValue || dto.TipoSangre is not null)
                paciente.Update(dto.Activo ?? paciente.Activo, dto.TipoSangre ?? paciente.TipoSangre);

            await _context.SaveChangesAsync();
        }
    }
}
