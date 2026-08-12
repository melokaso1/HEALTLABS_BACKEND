using Application.DTOs.Antecedente;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedentes
{
    public class CreateAntecedenteUseCase
    {
        private readonly IGenericRepository<AntecedenteEntity> _repo;

        public CreateAntecedenteUseCase(IGenericRepository<AntecedenteEntity> repo)
        {
            _repo = repo;
        }

        public async Task<AntecedenteEntity> ExecuteAsync(CreateAntecedenteDto dto)
        {
            var antecedente = new AntecedenteEntity(
                dto.PacienteId,
                dto.Tipo,
                dto.Descripcion,
                dto.UsuarioRegistroId);

            return await _repo.AddAsync(antecedente);
        }
    }
}
