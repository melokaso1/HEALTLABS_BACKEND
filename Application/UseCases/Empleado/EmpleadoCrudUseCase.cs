using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Empleado;

public sealed class EmpleadoCrudUseCase : EntityCrudUseCase<EmpleadoEntity>
{
    public EmpleadoCrudUseCase(IGenericRepository<EmpleadoEntity> repository) : base(repository) { }
}
