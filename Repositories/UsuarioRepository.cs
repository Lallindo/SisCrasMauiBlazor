using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class UsuarioRepository(SisCrasDbContext dbContext) : EfRepository<Usuario>(dbContext), IUsuarioRepository
{
    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await DbContext.Usuarios
            .Where(u => u.Id == id)
            .SelectMany(u => u.FamiliaUsuarios)
            .Where(fu => fu.Ativo)
            .Select(fu => fu.Familia)
            .FirstOrDefaultAsync();
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario)
    {
        return await GetActiveFamiliaFromUsuario(usuario.Id);
    }

    public async Task<Usuario?> GetByCpf(string cpfParaBuscar)
    {
        return await DbContext.Usuarios
            .Where(u => u.Cpf == cpfParaBuscar)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetByCpf(Usuario usuario)
    {
        return await GetByCpf(usuario.Cpf);
    }

    public async Task<Usuario?> GetByNome(string nome)
    {
        return await DbContext.Usuarios
            .Where(u => u.Nome.Contains(nome))
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetByNome(Usuario usuario)
    {
        return await GetByNome(usuario.Nome);
    }

    public async Task<Usuario?> GetByDataNascimento(DateOnly dataNascimento)
    {
        return await DbContext.Usuarios
            .Where(u => u.DataNascimento == dataNascimento)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetByDataNascimento(Usuario usuario)
    {
        return await GetByDataNascimento(usuario.DataNascimento);
    }

    public async Task<List<Familia>> GetFamiliasFromUsuario(int id)
    {
        return await DbContext.Familias
            .Include(f => f.Prontuarios)
            .Where(f => f.FamiliaUsuarios.Any(fu => fu.UsuarioId == id))
            .ToListAsync();
    }

    public async Task<List<Familia>> GetFamiliasFromUsuario(Usuario usuario)
    {
        return await GetFamiliasFromUsuario(usuario.Id);
    }
}