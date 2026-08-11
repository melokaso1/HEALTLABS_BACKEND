using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento
{
    public class GetAllTipoDocumentoUseCase
    {
        private readonly IGenericRepository<TipoDocumentoEntity> _repo;

        public GetAllTipoDocumentoUseCase(IGenericRepository<TipoDocumentoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TipoDocumentoEntity>> ExecuteAsync()
        {
            return await _repo.GetAllEntitiesAsync();
        }
    }
}