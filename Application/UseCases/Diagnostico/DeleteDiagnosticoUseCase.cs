using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Diagnostico
{
    public class DeleteDiagnosticoUseCase
    {
        private readonly IGenericRepository<DiagnosticoEntity> _repo;
        public DeleteDiagnosticoUseCase(IGenericRepository<DiagnosticoEntity> repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Guid id)
        {

            await _repo.DeleteAsync(id);
        }
    }
}