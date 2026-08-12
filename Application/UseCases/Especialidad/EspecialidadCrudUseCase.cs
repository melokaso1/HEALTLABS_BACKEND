using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Especialidad;

public sealed class EspecialidadCrudUseCase : EntityCrudUseCase<EspecialidadEntity>
{
    public EspecialidadCrudUseCase(IGenericRepository<EspecialidadEntity> repository) : base(repository) { }
}
