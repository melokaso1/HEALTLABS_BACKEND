using Application.DTOs.CitaHistorialEstado;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado
{
    public class CreateCitaHistorialEstadoUseCase
    {
        private readonly IGenericRepository<CitaHistorialEstadoEntity> _repo;

        public CreateCitaHistorialEstadoUseCase(IGenericRepository<CitaHistorialEstadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CitaHistorialEstadoEntity> ExecuteAsync(CreateCitaHistorialEstadoDto dto)
        {
            var entity = new CitaHistorialEstadoEntity(
                dto.CitaId,
                dto.EstadoAnteriorId,
                dto.EstadoNuevoId,
                dto.UsuarioId,
                dto.Observacion);
            return await _repo.AddAsync(entity);
        }
    }
}
