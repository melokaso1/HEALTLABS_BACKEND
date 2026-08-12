using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Antecedente;

public sealed class AntecedenteCrudUseCase : EntityCrudUseCase<AntecedenteEntity>
{
    public AntecedenteCrudUseCase(IGenericRepository<AntecedenteEntity> repository) : base(repository) { }
}
