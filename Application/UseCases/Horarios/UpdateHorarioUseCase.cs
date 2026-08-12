using Application.DTOs.Horario;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class UpdateHorarioUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;

        public UpdateHorarioUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateHorarioDto dto)
        {
            var horario = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            if (await _repo.AnyAsync(h => h.MedicoId == dto.MedicoId && h.Id != id))
                throw new InvalidOperationException("El médico ya tiene una jornada configurada.");

            horario.Update(
                dto.MedicoId,
                dto.HoraEntrada,
                dto.HoraSalida,
                dto.SalidaAlmuerzo,
                dto.RetornoActividades);

            await _repo.UpdateAsync(horario);
        }
    }
}
