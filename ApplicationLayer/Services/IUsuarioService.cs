using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface IUsuarioService : IBaseService<Usuario>
{
    Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario);
    Task<List<Familia?>> GetFamiliasFromUsuario(int id);
    Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario);
    Task<Familia?> GetActiveFamiliaFromUsuario(int id);
}