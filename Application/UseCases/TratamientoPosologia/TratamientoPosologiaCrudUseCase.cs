using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TratamientoPosologia;

public sealed class TratamientoPosologiaCrudUseCase : EntityCrudUseCase<TratamientoPosologiaEntity>
{
    public TratamientoPosologiaCrudUseCase(IGenericRepository<TratamientoPosologiaEntity> repository) : base(repository) { }
}
