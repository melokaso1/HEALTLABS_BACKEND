using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Domain.Interfaces;

namespace Application.UseCases.Common;

public abstract class EntityCrudUseCase<TEntity> where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;

    protected EntityCrudUseCase(IGenericRepository<TEntity> repository) => _repository = repository;

    public Task<IEnumerable<TEntity>> GetAllAsync() => _repository.GetAllEntitiesAsync();
    public Task<TEntity?> GetByIdAsync(Guid id) => _repository.GetEntityByIdAsync(id);
    public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => _repository.FindAsync(predicate);

    public async Task<TEntity> CreateAsync<TDto>(TDto dto)
    {
        var entity = CreateEntity(dto!);
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync<TDto>(Guid id, TDto dto)
    {
        var entity = await _repository.GetEntityByIdAsync(id)
            ?? throw new KeyNotFoundException("El registro solicitado no existe.");
        CopyProperties(dto!, entity);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (await _repository.GetEntityByIdAsync(id) is null)
            throw new KeyNotFoundException("El registro solicitado no existe.");
        await _repository.DeleteAsync(id);
    }

    private static TEntity CreateEntity(object dto)
    {
        var dtoProperties = dto.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
        var constructor = typeof(TEntity).GetConstructors()
            .OrderByDescending(c => c.GetParameters().Count(p => dtoProperties.ContainsKey(p.Name!)))
            .FirstOrDefault();

        TEntity entity;
        if (constructor is not null)
        {
            var arguments = constructor.GetParameters()
                .Select(p => dtoProperties.TryGetValue(p.Name!, out var property)
                    ? property.GetValue(dto)
                    : p.HasDefaultValue ? p.DefaultValue : DefaultValue(p.ParameterType))
                .ToArray();
            entity = (TEntity)constructor.Invoke(arguments);
        }
        else
        {
            entity = (TEntity)RuntimeHelpers.GetUninitializedObject(typeof(TEntity));
            typeof(TEntity).GetProperty("Id")?.SetValue(entity, Guid.NewGuid());
        }

        CopyProperties(dto, entity);
        return entity;
    }

    private static void CopyProperties(object source, TEntity target)
    {
        var targetProperties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && p.Name != "Id")
            .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
        foreach (var sourceProperty in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            if (sourceProperty.CanRead && targetProperties.TryGetValue(sourceProperty.Name, out var targetProperty)
                && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                targetProperty.SetValue(target, sourceProperty.GetValue(source));
    }

    private static object? DefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
}
