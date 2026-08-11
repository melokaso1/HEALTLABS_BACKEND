using Application.DTOs.TipoDocumento;
using Application.DTOs.Tratamiento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento
{
    public class UpdateTratamientoUseCase
    {
        private readonly IGenericRepository<TratamientoEntity> _repo;

        public UpdateTratamientoUseCase(IGenericRepository<TratamientoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateTratamientoDto dto)
        {
            var tratamiento = await _repo.GetEntityByIdAsync(id);

            if (tratamiento == null)
            {
                throw new ArgumentException("No Existe");
            }

            tratamiento.Codigo = dto.Codigo;
            tratamiento.Nombre = dto.Nombre;
            tratamiento.Descripcion = dto.Descripcion;
            tratamiento.Activo = dto.Activo;

            await _repo.UpdateAsync(tratamiento);

        }
    }
}