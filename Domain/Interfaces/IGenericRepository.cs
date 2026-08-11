using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(Guid id);
        public Task<TEntity> GetEntityByIdAsync(Guid id);
        public Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
    }
}
