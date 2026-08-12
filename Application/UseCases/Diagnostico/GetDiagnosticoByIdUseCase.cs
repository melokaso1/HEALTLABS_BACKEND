using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Diagnostico
{
    public class GetDiagnosticoByIdUseCase
    {
        private readonly IGenericRepository<DiagnosticoEntity> _repo;

        public GetDiagnosticoByIdUseCase(IGenericRepository<DiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<DiagnosticoEntity> ExecuteAsync(Guid id)
        {
            var entity = await _repo.GetEntityByIdAsync(id)
                ?? throw new KeyNotFoundException("El diagnóstico no existe.");
            return entity;
        }
    }
}