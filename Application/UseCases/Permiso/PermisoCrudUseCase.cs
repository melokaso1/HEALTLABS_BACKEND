using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Permiso;

public sealed class PermisoCrudUseCase : EntityCrudUseCase<PermisoEntity>
{
    public PermisoCrudUseCase(IGenericRepository<PermisoEntity> repository) : base(repository) { }
}
