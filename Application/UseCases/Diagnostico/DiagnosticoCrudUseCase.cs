using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Diagnostico;

public sealed class DiagnosticoCrudUseCase : EntityCrudUseCase<DiagnosticoEntity>
{
    public DiagnosticoCrudUseCase(IGenericRepository<DiagnosticoEntity> repository) : base(repository) { }
}
