using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tratamiento;

public sealed class TratamientoCrudUseCase : EntityCrudUseCase<TratamientoEntity>
{
    public TratamientoCrudUseCase(IGenericRepository<TratamientoEntity> repository) : base(repository) { }
}
