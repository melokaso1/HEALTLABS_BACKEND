using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
        public Task<TEntity> GetEntityByIdAsync();
        public Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
    }
}
