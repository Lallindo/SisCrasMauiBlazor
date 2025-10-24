using SisCras.Models;

namespace SisCras.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<List<Familia?>> GetFamiliasFromUsuario(int id);
    Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario);
    Task<Familia?> GetActiveFamiliaFromUsuario(int id);
    Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario);
    Task<Usuario?> GetByCpf(string cpfParaBuscar);
    Task<Usuario?> GetByCpf(Usuario usuario);
    Task<Usuario?> GetByNome(string nome);
    Task<Usuario?> GetByNome(Usuario usuario);
    Task<Usuario?> GetByDataNascimento(DateOnly dataNascimento);
    Task<Usuario?> GetByDataNascimento(Usuario usuario);
}