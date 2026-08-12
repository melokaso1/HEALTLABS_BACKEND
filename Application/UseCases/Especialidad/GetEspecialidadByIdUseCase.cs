using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Especialidad
{
    public class GetEspecialidadByIdUseCase
    {
        private readonly IGenericRepository<EspecialidadEntity> _repo;

        public GetEspecialidadByIdUseCase(IGenericRepository<EspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EspecialidadEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("La especialidad no existe.");
            return entity;
        }
    }
}