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
            var usuario = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El usuario solicitado no existe.");

            var passwordHash = usuario.PasswordHash;
            var passwordChangedAt = usuario.PasswordChangedAt;
            var tokenVersion = usuario.TokenVersion;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                PasswordValueObject.Create(dto.Password);
                passwordHash = _passwordHasher.Hash(dto.Password);
                passwordChangedAt = DateTime.UtcNow;
                tokenVersion++;
            }

            usuario.Update(
                dto.RolId,
                dto.Username,
                dto.Email,
                passwordHash,
                dto.Activo,
                usuario.UltimoLogin,
                usuario.IntentosFallidos,
                usuario.BloqueadoHasta,
                dto.DebeCambiarPassword,
                passwordChangedAt,
                tokenVersion);

            await _repo.UpdateAsync(usuario);
        }
    }
}
