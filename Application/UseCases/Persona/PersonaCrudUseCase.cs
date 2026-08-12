using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Persona;

public sealed class PersonaCrudUseCase : EntityCrudUseCase<PersonaEntity>
{
    public PersonaCrudUseCase(IGenericRepository<PersonaEntity> repository) : base(repository) { }
}
