using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo
{
    public class GetSexoByIdUseCase
    {
        private readonly IGenericRepository<SexoEntity> _repo;

        public GetSexoByIdUseCase(IGenericRepository<SexoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<SexoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El sexo no existe.");
            return entity;
        }
    }
}
