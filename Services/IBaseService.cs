namespace SisCras.Services;

public interface IBaseService<T>
{
    Task<ICollection<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    // Task<T?> GetByIdAsync(T obj); FIXME Deve ser usado assim?
    Task<T> AddAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(T obj);
}