using System.Linq.Expressions;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró la entidad con id {id}.");
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> GetEntityByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
    {
        if (typeof(TEntity) == typeof(Domain.Entities.MedicoEntity))
        {
            return (IEnumerable<TEntity>)await Context.Medicos
                .Include(m => m.Empleado!)
                    .ThenInclude(e => e.Persona!)
                .Include(m => m.Especialidades!)
                    .ThenInclude(me => me.Especialidad!)
                .AsNoTracking()
                .ToListAsync();
        }

        if (typeof(TEntity) == typeof(Domain.Entities.PacienteEntity))
        {
            return (IEnumerable<TEntity>)await Context.Pacientes
                .Include(p => p.Persona!)
                    .ThenInclude(pers => pers.TipoDocumento!)
                .Include(p => p.Persona!)
                    .ThenInclude(pers => pers.Sexo!)
                .Include(p => p.Persona!)
                    .ThenInclude(pers => pers.Telefonos!)
                .AsNoTracking()
                .ToListAsync();
        }

        if (typeof(TEntity) == typeof(Domain.Entities.UsuarioEntity))
        {
            return (IEnumerable<TEntity>)await Context.Usuarios
                .Include(u => u.Empleado!)
                    .ThenInclude(e => e.Persona!)
                .Include(u => u.Rol!)
                .AsNoTracking()
                .ToListAsync();
        }

        if (typeof(TEntity) == typeof(Domain.Entities.CitaEntity))
        {
            return (IEnumerable<TEntity>)await Context.Citas
                .AsNoTracking()
                .ToListAsync();
        }

        return await DbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AnyAsync(predicate);
    }

    public virtual async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}
