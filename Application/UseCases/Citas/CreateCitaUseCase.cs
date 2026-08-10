using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Application.DTOs.Cita;

namespace Application.UseCases.Citas
{
    public class CreateCitaUseCase
    {
        private readonly IGenericRepository<CitaEntity> _repo;

        public CreateCitaUseCase(IGenericRepository<CitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CitaEntity> ExecuteAsync(CreateCitaDto dto)
        {
            var cita = new CitaEntity(
                                        dto.PacienteId,
                                        dto.MedicoId,
                                        dto.EstadoCitaId,
                                        dto.Fecha,
                                        dto.HoraInicio,
                                        dto.HoraFin,
                                        dto.MotivoConsulta,
                                        dto.Observaciones,
                                        dto.IdUsuarioCreacion
                                        );
     
            return await _repo.AddAsync(cita);
        }
    }
}
