using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Sexo;

public sealed class SexoCrudUseCase : EntityCrudUseCase<SexoEntity>
{
    public SexoCrudUseCase(IGenericRepository<SexoEntity> repository) : base(repository) { }
}
