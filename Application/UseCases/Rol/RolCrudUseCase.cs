using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Rol;

public sealed class RolCrudUseCase : EntityCrudUseCase<RolEntity>
{
    public RolCrudUseCase(IGenericRepository<RolEntity> repository) : base(repository) { }
}
