using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Citas
{
    internal class GetCitaByIdUseCase
    {
        private readonly IGenericRepository<CitaEntity> _repo;

        public GetCitaByIdUseCase(IGenericRepository<CitaEntity> repo)
        {
            _repo = repo;
        }

        public async Task<CitaEntity> ExecuteAsync(Guid id)
        {
            var cita = await _repo.GetEntityByIdAsync(id);

            if (cita is null)
                throw new KeyNotFoundException($"No se encontró la cita con id {id}");

            return cita;
        }
    }
}