using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Paciente;

public sealed class PacienteCrudUseCase : EntityCrudUseCase<PacienteEntity>
{
    public PacienteCrudUseCase(IGenericRepository<PacienteEntity> repository) : base(repository) { }
}
