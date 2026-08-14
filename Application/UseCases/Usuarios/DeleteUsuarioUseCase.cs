using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios
{
    public class DeleteUsuarioUseCase
    {
        private readonly IGenericRepository<UsuarioEntity> _repo;
        public DeleteUsuarioUseCase(IGenericRepository<UsuarioEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {
            var usuario = await _repo.GetEntityByIdAsync(id);
            if (usuario != null)
            {
                usuario.Activo = false;
                await _repo.UpdateAsync(usuario);
            }
        }
    }
}
