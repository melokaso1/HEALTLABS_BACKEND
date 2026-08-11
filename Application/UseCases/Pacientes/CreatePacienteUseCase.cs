using Application.DTOs.Cita;
using Application.DTOs.Paciente;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Pacientes
{
    public class CreatePacienteUseCase
    {
        private readonly IGenericRepository<PacienteEntity> _repo;

        public CreatePacienteUseCase(IGenericRepository<PacienteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PacienteEntity> ExecuteAsync(CreatePacienteDto dto)
        {
            var paciente = new PacienteEntity(
                                        dto.PersonaId,
                                        dto.Activo
                                        );

            return await _repo.AddAsync(paciente);
        }
    }
}