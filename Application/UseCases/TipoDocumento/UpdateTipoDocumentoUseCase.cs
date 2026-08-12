using Application.DTOs.TipoDocumento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento
{
    public class UpdateTipoDocumentoUseCase
    {
        private readonly IGenericRepository<TipoDocumentoEntity> _repo;

        public UpdateTipoDocumentoUseCase(IGenericRepository<TipoDocumentoEntity> repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, UpdateTipoDocumentoDto dto)
        {
            var tipo_documento = await _repo.GetEntityByIdAsync(id)
                ?? throw new ArgumentException("No Existe");

            tipo_documento.Update(dto.Nombre);
            await _repo.UpdateAsync(tipo_documento);
        }
    }
}
