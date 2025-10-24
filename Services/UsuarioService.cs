using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository) : BaseService<Usuario>(usuarioRepository), IUsuarioService
{
    private IUsuarioRepository UsuarioRepository { get; } = usuarioRepository;
    public async Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(usuario);
    }
    public async Task<List<Familia?>> GetFamiliasFromUsuario(int id)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(id);
    }
    public async Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(usuario);
    }
    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(id);
    }
}