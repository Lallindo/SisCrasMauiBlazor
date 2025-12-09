using Microsoft.EntityFrameworkCore;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class BaseRepository<T>(SisCrasDbContext dbContext) : IRepository<T> where T : class
{
    protected readonly SisCrasDbContext DbContext = dbContext;

    public async Task<T> AddAsync(T obj, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<T>().AddAsync(obj, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

        return obj;
    }

    public async Task DeleteAsync(T obj, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Remove(obj);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var keyValues = new object[] { id };
        return await DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
    }

    public async Task UpdateAsync(T obj, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Update(obj);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
