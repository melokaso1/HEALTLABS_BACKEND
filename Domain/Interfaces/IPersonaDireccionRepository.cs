using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersonaDireccionRepository : IGenericRepository<PersonaDireccionEntity>
    {
        Task<IEnumerable<PersonaDireccionEntity>> GetEntityByPersonIdAsync(Guid personaId);
    }
}
