using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.EstadoCita;

public sealed class EstadoCitaCrudUseCase : EntityCrudUseCase<EstadoCitaEntity>
{
    public EstadoCitaCrudUseCase(IGenericRepository<EstadoCitaEntity> repository) : base(repository) { }
}
