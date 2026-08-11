using Application.DTOs.Cita;
using Domain.Entities;
using Domain.Interfaces;
using System.Xml;

namespace Application.UseCases.Citas
{
    public class UpdateCitaUseCase
    {
        private readonly IGenericRepository<CitaEntity> _repo;

        public UpdateCitaUseCase(IGenericRepository<CitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateCitaDto dto)
        {
            var cita = await _repo.GetEntityByIdAsync(id);

            if (cita == null) 
            {
                throw new ArgumentException("No Existe");
            }

            cita.PacienteId = dto.PacienteId;
            cita.MedicoId = dto.MedicoId;
            cita.EstadoCitaId = dto.EstadoCitaId;
            cita.TipoCitaId = dto.TipoCitaId;
            cita.Fecha = dto.Fecha;
            cita.HoraInicio = dto.HoraInicio;
            cita.HoraFin = dto.HoraFin;
            cita.MotivoConsulta = dto.MotivoConsulta;
            cita.Observaciones = dto.Observaciones;

            await _repo.UpdateAsync(cita);

        }
    }
}