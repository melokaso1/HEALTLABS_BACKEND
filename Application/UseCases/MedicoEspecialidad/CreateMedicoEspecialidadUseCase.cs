using Application.DTOs.MedicoEspecialidad;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad
{
    public class CreateMedicoEspecialidadUseCase
    {
        private readonly IGenericRepository<MedicoEspecialidadEntity> _repo;

        public CreateMedicoEspecialidadUseCase(IGenericRepository<MedicoEspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<MedicoEspecialidadEntity> ExecuteAsync(CreateMedicoEspecialidadDto dto)
        {
            var entity = new MedicoEspecialidadEntity(dto.MedicoId, dto.EspecialidadId, dto.Principal);
            return await _repo.AddAsync(entity);
        }
    }
}
