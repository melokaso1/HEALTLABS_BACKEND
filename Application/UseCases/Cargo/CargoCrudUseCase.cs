using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Cargo;

public sealed class CargoCrudUseCase : EntityCrudUseCase<CargoEntity>
{
    public CargoCrudUseCase(IGenericRepository<CargoEntity> repository) : base(repository) { }
}
