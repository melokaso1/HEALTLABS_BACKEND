using Application.DTOs.Antecedente;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class UpdateAntecedenteUseCase
    {
        private readonly IGenericRepository<AntecedenteEntity> _repo;

        public UpdateAntecedenteUseCase(IGenericRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateAntecedenteDto dto)
        {
            var antecedente = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            antecedente.Update(dto.Tipo, dto.Descripcion);
            await _repo.UpdateAsync(antecedente);
        }
    }
}
