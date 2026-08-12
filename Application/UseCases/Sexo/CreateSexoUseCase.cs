using Application.DTOs.Sexo;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo
{
    public class CreateSexoUseCase
    {
        private readonly IGenericRepository<SexoEntity> _repo;

        public CreateSexoUseCase(IGenericRepository<SexoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<SexoEntity> ExecuteAsync(CreateSexoDto dto)
        {
            var entity = new SexoEntity(dto.Codigo, dto.Nombre);
            return await _repo.AddAsync(entity);
        }
    }
}
