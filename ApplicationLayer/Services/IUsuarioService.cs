using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface IUsuarioService : IBaseService<Usuario>
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
    Task<List<Prontuario?>> GetAllProntuariosByUsuarioSearch(string? nome, string? cpf, string? nis);
    Task<List<Prontuario?>> GetAllProntuariosByUsuarioSearch(Usuario usuario);
    Task<Usuario?> GetUsuarioByUsuarioSearch(string? nome, string? cpf, string? nis);
    Task<Usuario?> GetUsuarioByUsuarioSearch(Usuario usuario);
    Task<FamiliaUsuario> DeactivateActiveFamiliaUsuario(int id);
    Task<FamiliaUsuario> DeactivateActiveFamiliaUsuario(Usuario usuario);
}
