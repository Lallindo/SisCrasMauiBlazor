using SisCras.Database;
using SisCras.Repositories;

namespace SisCras.Services;

public class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class
{
    protected IRepository<T> _Repository { get; } = repository;

    virtual async public Task<T> AddAsync(T obj)
    {
        return await _Repository.AddAsync(obj);
    }

    virtual async public Task DeleteAsync(T obj)
    {
        await _Repository.DeleteAsync(obj); 
    }

    virtual async public Task<ICollection<T>?> GetAllAsync()
    {
        return await _Repository.GetAllAsync();
    }

    virtual async public Task<T?> GetByIdAsync(int id)
    {
        return await _Repository.GetByIdAsync(id);
    }

    virtual async public Task UpdateAsync(T obj)
    {
        await _Repository.UpdateAsync(obj);
    }
}