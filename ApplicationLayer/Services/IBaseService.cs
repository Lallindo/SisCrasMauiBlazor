namespace SisCras.ApplicationLayer.Services;

public interface IBaseService<T>
{
    Task<ICollection<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(T obj);
}