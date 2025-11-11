using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class
{
    private IRepository<T> Repository { get; } = repository;

    public virtual async Task<T> AddAsync(T obj)
    {
        return await Repository.AddAsync(obj);
    }

    public virtual async Task DeleteAsync(T obj)
    {
        await Repository.DeleteAsync(obj);
    }

    public virtual async Task<ICollection<T>?> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public virtual async Task UpdateAsync(T obj)
    {
        await Repository.UpdateAsync(obj);
    }
}