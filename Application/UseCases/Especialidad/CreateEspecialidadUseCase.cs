using Application.DTOs.Cita;
using Application.DTOs.Especialidad;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Especialidad
{
    public class CreateEspecialidadUseCase
    {
        private readonly IGenericRepository<EspecialidadEntity> _repo;

        public CreateEspecialidadUseCase(IGenericRepository<EspecialidadEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EspecialidadEntity> ExecuteAsync(CreateEspecialidadDto dto)
        {
            var especialidad = new EspecialidadEntity(
                                        dto.Nombre,
                                        dto.Descripcion
                                        );

            return await _repo.AddAsync(especialidad);
        }
    }
}