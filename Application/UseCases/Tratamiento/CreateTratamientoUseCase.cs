using Application.DTOs.TipoDocumento;
using Application.DTOs.Tratamiento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class CreateTratamientoUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;

        public CreateTratamientoUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TratamientoEntity> ExecuteAsync(CreateTratamientoDto dto)
        {
            var tratamiento = new TratamientoEntity(
                                        dto.Codigo,
                                        dto.Nombre,
                                        dto.Descripcion,
                                        dto.Activo
                                        );

            return await _repo.AddAsync(tratamiento);
        }
    }
}