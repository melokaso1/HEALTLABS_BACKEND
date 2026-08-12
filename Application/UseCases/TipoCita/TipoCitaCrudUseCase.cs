using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.TipoCita;

public sealed class TipoCitaCrudUseCase : EntityCrudUseCase<TipoCitaEntity>
{
    public TipoCitaCrudUseCase(IGenericRepository<TipoCitaEntity> repository) : base(repository) { }
}
