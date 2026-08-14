using Application.DTOs.Usuario;
using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Usuarios;

public sealed class CreateUsuarioCompletoUseCase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUsuarioCompletoUseCase(AppDbContext context, IPasswordHasher passwordHasher)
        => (_context, _passwordHasher) = (context, passwordHasher);

    public async Task<UsuarioDto> ExecuteAsync(CreateUsuarioCompletoDto dto)
    {
        var documento = NumeroDocumentoValueObject.Create(dto.Persona.NumeroDocumento).Value;
        var username = UsernameValueObject.Create(dto.Username).Value;
        var password = PasswordValueObject.Create(dto.Password).Value;

        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var rol = await _context.Roles.SingleOrDefaultAsync(r => r.Id == dto.RolId && r.Activo)
                ?? throw new InvalidOperationException("El rol especificado no existe o está inactivo.");
            var cargoCodigo = rol.NombreRol switch
            {
                RolValueObject.Administrador => "ADM",
                RolValueObject.Profesional => "MED",
                RolValueObject.Recepcionista => "REC",
                _ => throw new InvalidOperationException("El rol especificado no está habilitado para crear personal.")
            };
            var cargo = await _context.Cargos.SingleOrDefaultAsync(c => c.Codigo == cargoCodigo)
                ?? throw new InvalidOperationException($"No existe el cargo {cargoCodigo} requerido para el rol.");

            if (rol.NombreRol is RolValueObject.Profesional or RolValueObject.Recepcionista)
            {
                if (string.IsNullOrWhiteSpace(dto.Telefono))
                    throw new InvalidOperationException("El teléfono es obligatorio para profesionales y recepcionistas.");
                if (string.IsNullOrWhiteSpace(dto.Direccion))
                    throw new InvalidOperationException("La dirección es obligatoria para profesionales y recepcionistas.");
            }

            if (rol.NombreRol == RolValueObject.Profesional)
            {
                if (string.IsNullOrWhiteSpace(dto.RegistroProfesional))
                    throw new InvalidOperationException("La licencia o registro profesional es obligatorio.");
                if (!dto.EspecialidadId.HasValue || !await _context.Especialidades.AnyAsync(e => e.Id == dto.EspecialidadId.Value))
                    throw new InvalidOperationException("La especialidad especificada no existe.");
            }

            if (!await _context.TiposDocumento.AnyAsync(t => t.Id == dto.Persona.TipoDocumentoId))
                throw new InvalidOperationException("El tipo de documento especificado no existe.");
            if (dto.Persona.SexoId.HasValue && !await _context.Sexos.AnyAsync(s => s.Id == dto.Persona.SexoId.Value))
                throw new InvalidOperationException("El sexo especificado no existe.");
            if (await _context.Personas.AnyAsync(p =>
                    p.TipoDocumentoId == dto.Persona.TipoDocumentoId && p.NumeroDocumento == documento))
                throw new DuplicateDocumentException();
            if (await _context.Usuarios.AnyAsync(u => u.Username == username))
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                throw new InvalidOperationException("El correo electrónico ya se encuentra registrado.");

            var persona = new PersonaEntity(dto.Persona.Nombre, dto.Persona.Apellido,
                dto.Persona.TipoDocumentoId, documento, dto.Persona.FechaNacimiento, dto.Persona.SexoId);
            var empleado = new EmpleadoEntity(persona.Id, cargo.Id, dto.FechaIngreso, null, dto.Activo);
            var usuario = new UsuarioEntity(empleado.Id, rol.Id, username, dto.Email, _passwordHasher.Hash(password),
                dto.Activo, null, 0, null, dto.DebeCambiarPassword, null, 1);

            _context.Personas.Add(persona);
            _context.Empleados.Add(empleado);
            _context.Usuarios.Add(usuario);
            if (!string.IsNullOrWhiteSpace(dto.Telefono))
                _context.PersonasTelefono.Add(new PersonaTelefonoEntity(persona.Id, dto.Telefono, dto.TipoTelefono, true));
            if (!string.IsNullOrWhiteSpace(dto.Direccion))
                _context.PersonasDireccion.Add(new PersonaDireccionEntity(persona.Id, dto.Direccion, dto.Ciudad, true));

            if (rol.NombreRol == RolValueObject.Profesional)
            {
                var medico = new MedicoEntity(empleado.Id, dto.RegistroProfesional!.Trim(), dto.Activo);
                _context.Medicos.Add(medico);
                _context.MedicosEspecialidad.Add(
                    new MedicoEspecialidadEntity(medico.Id, dto.EspecialidadId!.Value, dto.EspecialidadPrincipal));
                empleado.Medico = medico;
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            usuario.Empleado = empleado;
            usuario.Rol = rol;
            empleado.Persona = persona;
            return UsuarioDtoMapper.ToDto(usuario);
        });
    }
}
