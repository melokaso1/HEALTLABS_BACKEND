using Application.DTOs.Tratamiento;
using Application.DTOs.Usuario;
using Domain.Entities;
using Domain.Interfaces;
/*
namespace Application.UseCases.Usuarios
{
    public class CreateUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;

        public CreateUsuarioUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<UsuarioEntity> ExecuteAsync(CreateUsuarioDto dto)
        {
             usuario = new UsuarioEntity(
                                        dto.EmpleadoId,
                                        dto.RolId,
                                        dto.Username,
                                        dto.Email,
                                        dto.Password,
                                        dto.Activo,
                                        dto.DebeCambiarPassword
                                        );

            return await _repo.AddAsync(usuario);
        }
    }
}
Hay que arreglar DTO porque no coincide con la entity
 */