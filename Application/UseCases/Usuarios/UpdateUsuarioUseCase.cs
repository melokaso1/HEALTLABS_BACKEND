using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Usuarios
{
    public class UpdateUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUsuarioUseCase(
            IGenericRepository<UsuarioEntity> repo,
            IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
        }

        public async Task ExecuteAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repo.GetEntityByIdAsync(id);

            if (usuario == null)
                throw new KeyNotFoundException("El usuario solicitado no existe.");

            usuario.EmpleadoId = dto.EmpleadoId;
            usuario.RolId = dto.RolId;
            usuario.Username = dto.Username;
            usuario.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                PasswordValueObject.Create(dto.Password);
                usuario.PasswordHash = _passwordHasher.Hash(dto.Password);
                usuario.PasswordChangedAt = DateTime.UtcNow;
                usuario.TokenVersion++;
            }
            usuario.Activo = dto.Activo;
            usuario.DebeCambiarPassword = dto.DebeCambiarPassword;

            await _repo.UpdateAsync(usuario);

        }
    }
}