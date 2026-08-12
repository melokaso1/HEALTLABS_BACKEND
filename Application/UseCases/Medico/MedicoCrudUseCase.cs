using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Medico;

public sealed class MedicoCrudUseCase : EntityCrudUseCase<MedicoEntity>
{
    public MedicoCrudUseCase(IGenericRepository<MedicoEntity> repository) : base(repository) { }
}
