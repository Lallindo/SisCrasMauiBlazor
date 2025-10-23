using SisCras.Models;

namespace SisCras.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario);
    Task<List<Familia?>> GetFamiliasFromUsuario(int id);
    Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario);
    Task<Familia?> GetActiveFamiliaFromUsuario(int id);
}