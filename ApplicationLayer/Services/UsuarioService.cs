using SisCras.Domain.Entities;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository)
    : BaseService<Usuario>(usuarioRepository), IUsuarioService
{
    private IUsuarioRepository UsuarioRepository { get; } = usuarioRepository;

    public async Task<List<Familia?>> GetFamiliasFromUsuario(int id)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(id);
    }

    public async Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(usuario);
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(id);
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(usuario);
    }

    public async Task<Usuario?> GetByCpf(string cpfParaBuscar)
    {
        return await UsuarioRepository.GetByCpf(cpfParaBuscar);
    }

    public async Task<Usuario?> GetByCpf(Usuario usuario)
    {
        return await UsuarioRepository.GetByCpf(usuario);
    }

    public async Task<Usuario?> GetByNome(string nome)
    {
        return await UsuarioRepository.GetByNome(nome);
    }

    public async Task<Usuario?> GetByNome(Usuario usuario)
    {
        return await UsuarioRepository.GetByNome(usuario);
    }

    public async Task<Usuario?> GetByDataNascimento(DateOnly dataNascimento)
    {
        return await UsuarioRepository.GetByDataNascimento(dataNascimento);
    }

    public async Task<Usuario?> GetByDataNascimento(Usuario usuario)
    {
        return await UsuarioRepository.GetByDataNascimento(usuario);
    }

    public async Task<List<Prontuario>> GetAllProntuariosByUsuarioSearch(string? nome, string? cpf, string? nis)
    {
        return await UsuarioRepository.GetAllProntuariosByUsuarioSearch(nome, cpf, nis);
    }

    public async Task<List<Prontuario?>> GetAllProntuariosByUsuarioSearch(Usuario usuario)
    {
        return await UsuarioRepository.GetAllProntuariosByUsuarioSearch(usuario);
    }
}