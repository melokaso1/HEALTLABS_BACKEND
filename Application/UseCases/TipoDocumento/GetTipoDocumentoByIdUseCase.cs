using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento
{
    public class GetTipoDocumentoByIdUseCase
    {
        private readonly IGenericRepository<TipoDocumentoEntity> _repo;

        public GetTipoDocumentoByIdUseCase(IGenericRepository<TipoDocumentoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TipoDocumentoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El tipo de documento no existe.");
            return entity;
        }
    }
}