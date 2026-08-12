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

            if (horario == null)
            {
                throw new ArgumentException("No Existe");
            }

            horario.HoraEntrada = dto.HoraEntrada;
            horario.HoraSalida = dto.HoraSalida;
            horario.SalidaAlmuerzo = dto.SalidaAlmuerzo;
            horario.RetornoActividades = dto.RetornoActividades;

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
