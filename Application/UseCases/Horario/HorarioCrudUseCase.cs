using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Horario;

public sealed class HorarioCrudUseCase : EntityCrudUseCase<HorarioEntity>
{
    public HorarioCrudUseCase(IGenericRepository<HorarioEntity> repository) : base(repository) { }
}
