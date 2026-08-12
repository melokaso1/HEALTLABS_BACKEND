using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CitaHistorialEstado;

public sealed class CitaHistorialEstadoCrudUseCase : EntityCrudUseCase<CitaHistorialEstadoEntity>
{
    public CitaHistorialEstadoCrudUseCase(IGenericRepository<CitaHistorialEstadoEntity> repository) : base(repository) { }
}
