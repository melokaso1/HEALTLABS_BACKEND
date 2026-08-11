using Application.DTOs.Cita;
using Application.DTOs.EstadoCita;
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
            var horario = new HorarioEntity(
                                        dto.MedicoId,
                                        dto.Fecha,
                                        dto.HoraEntrada,
                                        dto.HoraSalida,
                                        dto.SalidaAlmuerzo,
                                        dto.RetornoActividades
                                        );

            return await _repo.AddAsync(horario);
        }
    }
}