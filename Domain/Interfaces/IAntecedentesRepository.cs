using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAntecedentesRepository : IGenericRepository<AntecedenteEntity>
    {
        Task<IEnumerable<AntecedenteEntity>> GetEntityByDateAsync(DateOnly fecha);
        Task<IEnumerable<AntecedenteEntity>> GetEntityByType(string tipo);
    }
}
