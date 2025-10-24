using SisCras.Repositories;

namespace SisCras.Services;

public class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class
{
    private IRepository<T> Repository { get; } = repository;

    virtual public async Task<T> AddAsync(T obj)
    {
        return await Repository.AddAsync(obj);
    }

    virtual public async Task DeleteAsync(T obj)
    {
        await Repository.DeleteAsync(obj);
    }

    virtual public async Task<ICollection<T>?> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    virtual public async Task<T?> GetByIdAsync(int id)
    {
        return await Repository.GetByIdAsync(id);
    }

    virtual public async Task UpdateAsync(T obj)
    {
        await Repository.UpdateAsync(obj);
    }
}