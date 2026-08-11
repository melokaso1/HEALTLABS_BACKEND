using Application.DTOs.Cita;
using Application.DTOs.Medico;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Medicos
{
    public class UpdateMedicoUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;

        public UpdateMedicoUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateMedicoDto dto)
        {
            var medico = await _repo.GetEntityByIdAsync(id);

            if (medico == null)
            {
                throw new ArgumentException("No Existe");
            }

            medico.EmpleadoId = dto.EmpleadoId;
            medico.RegistroProfesional = dto.RegistroProfesional;
            medico.Activo = dto.Activo;

            await _repo.UpdateAsync(medico);

        }
    }
}