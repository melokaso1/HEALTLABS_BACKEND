using Application.Common;
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
            TimePrecision.EnsureHhMm(
                ("HoraEntrada", dto.HoraEntrada),
                ("HoraSalida", dto.HoraSalida),
                ("SalidaAlmuerzo", dto.SalidaAlmuerzo),
                ("RetornoActividades", dto.RetornoActividades));

            var horario = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("No Existe");

            horario.Update(
                dto.HoraEntrada,
                dto.HoraSalida,
                dto.SalidaAlmuerzo,
                dto.RetornoActividades);

            await _repo.UpdateAsync(horario);
        }
    }
}
