using Application.DTOs.TipoCita;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita
{
    public class CreateTipoCitaUseCase
    {
        private readonly IGenericRepository<TipoCitaEntity> _repo;

        public CreateTipoCitaUseCase(IGenericRepository<TipoCitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TipoCitaEntity> ExecuteAsync(CreateTipoCitaDto dto)
        {
            var entity = new TipoCitaEntity(dto.Codigo, dto.Nombre, dto.DuracionMinutos, dto.Activo);
            return await _repo.AddAsync(entity);
        }
    }
}
