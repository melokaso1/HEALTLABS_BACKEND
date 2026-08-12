using Application.DTOs.MedicoEspecialidad;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad
{
    public class UpdateMedicoEspecialidadUseCase
    {
        private readonly IGenericRepository<MedicoEspecialidadEntity> _repo;

        public UpdateMedicoEspecialidadUseCase(IGenericRepository<MedicoEspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateMedicoEspecialidadDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.MedicoId, dto.EspecialidadId, dto.Principal);
            await _repo.UpdateAsync(entity);
        }
    }
}
