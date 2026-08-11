using Application.DTOs.Cita;
using Application.DTOs.Empleado;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Empleados
{
    public class CreateEmpleadoUseCase
    {
        private readonly IGenericRepository<EmpleadoEntity> _repo;

        public CreateEmpleadoUseCase(IGenericRepository<EmpleadoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<EmpleadoEntity> ExecuteAsync(CreateEmpleadoDto dto)
        {
            var empleado = new EmpleadoEntity(
                                        dto.PersonaId,
                                        dto.CargoId,
                                        dto.FechaIngreso,
                                        dto.FechaRetiro,
                                        dto.Activo
                                        );

            return await _repo.AddAsync(empleado);
        }
    }
}