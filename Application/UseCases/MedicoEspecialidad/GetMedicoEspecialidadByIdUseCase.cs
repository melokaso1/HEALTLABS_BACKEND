using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad
{
    public class GetMedicoEspecialidadByIdUseCase
    {
        private readonly IGenericRepository<MedicoEspecialidadEntity> _repo;

        public GetMedicoEspecialidadByIdUseCase(IGenericRepository<MedicoEspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<MedicoEspecialidadEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El médico-especialidad no existe.");
            return entity;
        }
    }
}
