using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AtencionTratamiento;

public sealed class AtencionTratamientoCrudUseCase : EntityCrudUseCase<AtencionTratamientoEntity>
{
    public AtencionTratamientoCrudUseCase(IGenericRepository<AtencionTratamientoEntity> repository) : base(repository) { }
}
