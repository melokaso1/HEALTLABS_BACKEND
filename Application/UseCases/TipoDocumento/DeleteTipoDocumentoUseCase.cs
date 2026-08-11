using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento
{
    public class DeleteTipoDocumentoUseCase
    {
        private readonly IGenericRepository<TipoDocumentoEntity> _repo;
        public DeleteTipoDocumentoUseCase(IGenericRepository<TipoDocumentoEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}