using Application.DTOs.Horario;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horarios
{
    public class CreateHorarioUseCase
    {
        private readonly IGenericRepository<HorarioEntity> _repo;

        public CreateHorarioUseCase(IGenericRepository<HorarioEntity> repo)
        {
            _repo = repo;
        }

        public async Task<HorarioEntity> ExecuteAsync(CreateHorarioDto dto)
        {
            if (await _repo.AnyAsync(h => h.MedicoId == dto.MedicoId))
                throw new InvalidOperationException("El médico ya tiene una jornada configurada.");

            var horario = new HorarioEntity(
                dto.MedicoId,
                dto.HoraEntrada,
                dto.HoraSalida,
                dto.SalidaAlmuerzo,
                dto.RetornoActividades);

            return await _repo.AddAsync(horario);
        }
    }
}
