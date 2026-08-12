using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaTelefono;

public sealed class PersonaTelefonoCrudUseCase : EntityCrudUseCase<PersonaTelefonoEntity>
{
    public PersonaTelefonoCrudUseCase(IGenericRepository<PersonaTelefonoEntity> repository) : base(repository) { }
}
