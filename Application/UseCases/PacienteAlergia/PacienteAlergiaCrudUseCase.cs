using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PacienteAlergia;

public sealed class PacienteAlergiaCrudUseCase : EntityCrudUseCase<PacienteAlergiaEntity>
{
    public PacienteAlergiaCrudUseCase(IGenericRepository<PacienteAlergiaEntity> repository) : base(repository) { }
}
