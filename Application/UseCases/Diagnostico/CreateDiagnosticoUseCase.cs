using Application.DTOs.Diagnostico;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Diagnostico
{
    public class CreateDiagnosticoUseCase
    {
        private readonly IGenericRepository<DiagnosticoEntity> _repo;

        public CreateDiagnosticoUseCase(IGenericRepository<DiagnosticoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<DiagnosticoEntity> ExecuteAsync(CreateDiagnosticoDto dto)
        {
            var diagnostico = new DiagnosticoEntity(
                                dto.CodigoCie10,
                                dto.Descripcion,
                                dto.Activo
                                );
            return await _repo.AddAsync(diagnostico);
        }
    }
}