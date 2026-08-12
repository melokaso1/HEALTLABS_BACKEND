using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoDocumento;

public sealed class TipoDocumentoCrudUseCase : EntityCrudUseCase<TipoDocumentoEntity>
{
    public TipoDocumentoCrudUseCase(IGenericRepository<TipoDocumentoEntity> repository) : base(repository) { }
}
