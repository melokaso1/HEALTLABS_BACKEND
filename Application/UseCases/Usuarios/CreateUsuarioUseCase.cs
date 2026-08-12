using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Usuarios
{
    public class CreateUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;
        private readonly IGenericRepository<EmpleadoEntity> _empleados;
        private readonly IGenericRepository<RolEntity> _roles;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUsuarioUseCase(
            IGenericRepository<UsuarioEntity> repo,
            IGenericRepository<EmpleadoEntity> empleados,
            IGenericRepository<RolEntity> roles,
            IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _empleados = empleados;
            _roles = roles;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioEntity> ExecuteAsync(CreateUsuarioDto dto)
        {
            if (!await _empleados.AnyAsync(e => e.Id == dto.EmpleadoId))
                throw new InvalidOperationException("El empleado especificado no existe.");

            if (!await _roles.AnyAsync(r => r.Id == dto.RolId))
                throw new InvalidOperationException("El rol especificado no existe.");

            if (await _repo.AnyAsync(u => u.Username == dto.Username))
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");

            if (await _repo.AnyAsync(u => u.Email == dto.Email))
                throw new InvalidOperationException("El correo electrónico ya se encuentra registrado.");

            PasswordValueObject.Create(dto.Password);

            var usuario = new UsuarioEntity(
                dto.EmpleadoId,
                dto.RolId,
                dto.Username,
                dto.Email,
                _passwordHasher.Hash(dto.Password),
                dto.Activo,
                null,
                0,
                null,
                dto.DebeCambiarPassword,
                null,
                1);

            return await _repo.AddAsync(usuario);
        }
    }
}
