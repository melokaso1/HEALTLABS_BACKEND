using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.MedicoEspecialidad;

public sealed class MedicoEspecialidadCrudUseCase : EntityCrudUseCase<MedicoEspecialidadEntity>
{
    public MedicoEspecialidadCrudUseCase(IGenericRepository<MedicoEspecialidadEntity> repository) : base(repository) { }
}
