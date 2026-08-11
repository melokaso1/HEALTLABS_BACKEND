using Application.DTOs.Cita;
using Application.DTOs.TipoDocumento;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento
{
    public class CreateTipoDocumentoUseCase
    {
        private readonly IGenericRepository<TipoDocumentoEntity> _repo;

        public CreateTipoDocumentoUseCase(IGenericRepository<TipoDocumentoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TipoDocumentoEntity> ExecuteAsync(CreateTipoDocumentoDto dto)
        {
            var tipo_documento = new TipoDocumentoEntity(
                                        dto.Codigo,
                                        dto.Nombre
                                        );

            return await _repo.AddAsync(tipo_documento);
        }
    }
}
