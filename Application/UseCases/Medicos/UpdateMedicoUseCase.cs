using Application.DTOs.Medico;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Medicos
{
    public class UpdateMedicoUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;

        public UpdateMedicoUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateMedicoDto dto)
        {
            var medico = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            medico.Update(dto.RegistroProfesional, dto.Activo);
            await _repo.UpdateAsync(medico);
        }
    }
}
