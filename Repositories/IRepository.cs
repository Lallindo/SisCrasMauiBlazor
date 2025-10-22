using Microsoft.EntityFrameworkCore;
using SisCras.Database;

namespace SisCras.Repositories;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T obj, CancellationToken cancellationToken = default);
    Task UpdateAsync(T obj, CancellationToken cancellationToken = default);
    Task DeleteAsync(T obj, CancellationToken cancellationToken = default); 
}