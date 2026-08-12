using Application.DTOs.Cita;
using Application.DTOs.Especialidad;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Especialidad
{
    public class UpdateEspecialidadUseCase
    {
        private readonly IGenericRepository<EspecialidadEntity> _repo;

        public UpdateEspecialidadUseCase(IGenericRepository<EspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateEspecialidadDto dto)
        {
            var especialidad = await _repo.GetEntityByIdAsync(id);

            if (especialidad == null)
            {
                throw new KeyNotFoundException("No Existe");
            }

            especialidad.Nombre = dto.Nombre;
            especialidad.Descripcion = dto.Descripcion;

            await _repo.UpdateAsync(especialidad);

        }
    }
}