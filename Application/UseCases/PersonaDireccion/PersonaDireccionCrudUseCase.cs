using Application.UseCases.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.PersonaDireccion;

public sealed class PersonaDireccionCrudUseCase : EntityCrudUseCase<PersonaDireccionEntity>
{
    public PersonaDireccionCrudUseCase(IGenericRepository<PersonaDireccionEntity> repository) : base(repository) { }
}
