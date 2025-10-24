using SisCras.Database;
using SisCras.Repositories;

namespace SisCras.Services;

public class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class
{
    private IRepository<T> Repository { get; } = repository;

    virtual async public Task<T> AddAsync(T obj)
    {
        return await Repository.AddAsync(obj);
    }

    virtual async public Task DeleteAsync(T obj)
    {
        await Repository.DeleteAsync(obj); 
    }

    virtual async public Task<ICollection<T>?> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    virtual async public Task<T?> GetByIdAsync(int id)
    {
        return await Repository.GetByIdAsync(id);
    }

    virtual async public Task UpdateAsync(T obj)
    {
        await Repository.UpdateAsync(obj);
    }
}