using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.RolPermiso;

public sealed class RolPermisoCrudUseCase : EntityCrudUseCase<RolPermisoEntity>
{
    public RolPermisoCrudUseCase(IGenericRepository<RolPermisoEntity> repository) : base(repository) { }
}
