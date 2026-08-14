using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Usuarios
{
    public class UpdateUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;
        private readonly IGenericRepository<RolEntity> _roles;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUsuarioUseCase(
            IGenericRepository<UsuarioEntity> repo,
            IGenericRepository<RolEntity> roles,
            IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _roles = roles;
            _passwordHasher = passwordHasher;
        }

        public async Task ExecuteAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El usuario solicitado no existe.");

            var rolId = dto.RolId == Guid.Empty ? usuario.RolId : dto.RolId;
            var username = string.IsNullOrWhiteSpace(dto.Username) ? usuario.Username : dto.Username;
            var email = string.IsNullOrWhiteSpace(dto.Email) ? usuario.Email : dto.Email;

            if (!await _roles.AnyAsync(r => r.Id == rolId))
                throw new InvalidOperationException("El rol especificado no existe.");

            if (await _repo.AnyAsync(u => u.Username == username && u.Id != id))
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");

            if (await _repo.AnyAsync(u => u.Email == email && u.Id != id))
                throw new InvalidOperationException("El correo electrónico ya se encuentra registrado.");

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

            if (rolId != usuario.RolId || (!dto.Activo && usuario.Activo))
                tokenVersion++;

            usuario.Update(
                rolId,
                username,
                email,
                passwordHash,
                dto.Activo,
                usuario.UltimoLogin,
                usuario.IntentosFallidos,
                usuario.BloqueadoHasta,
                dto.DebeCambiarPassword,
                passwordChangedAt,
                tokenVersion);

            try
            {
                await _repo.UpdateAsync(usuario);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    ex.InnerException?.Message ?? ex.Message,
                    ex);
            }
        }
    }
}
