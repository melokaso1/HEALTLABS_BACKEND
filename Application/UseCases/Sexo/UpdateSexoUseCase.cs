using Application.DTOs.Sexo;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo
{
    public class UpdateSexoUseCase
    {
        private readonly IGenericRepository<SexoEntity> _repo;

        public UpdateSexoUseCase(IGenericRepository<SexoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateSexoDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            entity.Update(dto.Nombre);
            await _repo.UpdateAsync(entity);
        }
    }
}
