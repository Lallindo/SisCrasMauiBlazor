
using Microsoft.EntityFrameworkCore;
using SisCras.Database;

namespace SisCras.Repositories;

public class EfRepository<T>(SisCrasDbContext dbContext) : IRepository<T> where T : class
{
    protected readonly SisCrasDbContext _DbContext = dbContext;

    public async Task<T> AddAsync(T obj, CancellationToken cancellationToken = default)
    {
        await _DbContext.Set<T>().AddAsync(obj, cancellationToken);
        await _DbContext.SaveChangesAsync(cancellationToken);

        return obj;
    }

    public async Task DeleteAsync(T obj, CancellationToken cancellationToken = default)
    {
        _DbContext.Set<T>().Remove(obj);
        await _DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _DbContext.Set<T>().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var keyValues = new object[] { id };
        return await _DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
    }

    public async Task UpdateAsync(T obj, CancellationToken cancellationToken = default)
    {
        _DbContext.Entry(obj).State = EntityState.Modified;
        await _DbContext.SaveChangesAsync(cancellationToken);
    }
}