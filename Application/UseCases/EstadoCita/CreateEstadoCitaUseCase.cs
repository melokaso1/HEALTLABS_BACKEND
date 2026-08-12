using Application.DTOs.EstadoCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita
{
    public class CreateEstadoCitaUseCase
    {
        private readonly IGenericRepository<EstadoCitaEntity> _repo;

        public CreateEstadoCitaUseCase(IGenericRepository<EstadoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EstadoCitaEntity> ExecuteAsync(CreateEstadoCitaDto dto)
        {
            var entity = new EstadoCitaEntity(dto.Codigo, dto.Descripcion);
            return await _repo.AddAsync(entity);
        }
    }
}
