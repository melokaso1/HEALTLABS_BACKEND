using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.DetalleDiagnostico;

public sealed class DetalleDiagnosticoCrudUseCase : EntityCrudUseCase<DetalleDiagnosticoEntity>
{
    public DetalleDiagnosticoCrudUseCase(IGenericRepository<DetalleDiagnosticoEntity> repository) : base(repository) { }
}
