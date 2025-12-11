using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class UsuarioRepository(SisCrasDbContext dbContext) : BaseRepository<Usuario>(dbContext), IUsuarioRepository
{
    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await DbContext.Usuarios
            .Where(u => u.Id == id)
            .SelectMany(u => u.FamiliaUsuarios)
            .Where(fu => fu.DataSaida == null)
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

    public async Task<Usuario?> GetUsuarioByUsuarioSearch(string? nome, string? cpf, string? nis)
    {
        if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf) && string.IsNullOrEmpty(nis)) return null;

        return await DbContext.Usuarios
            .Select(u => u)
            .Where(u => u.Nome == nome && u.Cpf == cpf && u.Nis == nis)
            .Include(u => u.FamiliaUsuarios)
            .ThenInclude(fu => fu.Familia)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetUsuarioByUsuarioSearch(Usuario usuario)
    {
        return await GetUsuarioByUsuarioSearch(usuario.Nome, usuario.Cpf, usuario.Nis);
    }

    public async Task<FamiliaUsuario?> DeactivateActiveFamiliaUsuario(int id)
    {
        var activeFamiliaUsuario = await DbContext.FamiliaUsuarios
            .Where(fu => fu.UsuarioId == id && fu.DataSaida == null)
            .FirstOrDefaultAsync();

        if (activeFamiliaUsuario == null) return null;
        
        activeFamiliaUsuario.DataSaida = DateOnly.FromDateTime(DateTime.Now);
        activeFamiliaUsuario.Parentesco = ParentescoEnum.Default;
        
        await UpdateAsync(activeFamiliaUsuario.Usuario);
        await DbContext.SaveChangesAsync();

        return activeFamiliaUsuario;
    }

    public async Task<FamiliaUsuario?> DeactivateActiveFamiliaUsuario(Usuario usuario)
    {
        return await DeactivateActiveFamiliaUsuario(usuario.Id);
    }

    public async Task<FamiliaUsuario> ReactivateFamiliaUsuario(int id)
    {
        var resp = await DbContext.FamiliaUsuarios
            .Where(fu => fu.Id == id)
            .Include(fu => fu.Usuario)
            .Include(fu => fu.Familia)
            .FirstOrDefaultAsync();

        // Se 'resp' for nulo, a linha 'resp.UsuarioId' logo abaixo causaria NRE. 
        // É uma boa prática verificar:
        if (resp == null)
        {
            // Trate o caso de ID inválido aqui, como retornar null ou lançar exceção.
            return null; 
        }

        var resp2 = await DbContext.FamiliaUsuarios
            .Where(fu => fu.UsuarioId == resp.UsuarioId && fu.DataSaida == null)
            .FirstOrDefaultAsync();

        // ** CORREÇÃO: Verifica se resp2 não é nulo antes de usá-lo. **
        if (resp2 != null)
        {
            resp2.DataSaida = DateOnly.FromDateTime(DateTime.Now);
            DbContext.FamiliaUsuarios.Update(resp2); 
        }
        resp.DataSaida = null;
        DbContext.FamiliaUsuarios.Update(resp);
        await DbContext.SaveChangesAsync();

        return resp;
    }

    public async Task<List<Prontuario>> GetAllProntuariosByUsuarioSearch(string? nome, string? cpf, string? nis)
    {
        if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf) && string.IsNullOrEmpty(nis)) return [];

        var query = DbContext.Prontuarios.AsQueryable();
        
        // Mantendo sua lógica de filtro OR/AND que corrigimos anteriormente
        query = query.Where(p => p.Familia.FamiliaUsuarios.Any(fu =>
            (string.IsNullOrEmpty(nome) || fu.Usuario.Nome.ToLower().Contains(nome.ToLower())) &&
            (string.IsNullOrEmpty(cpf)  || fu.Usuario.Cpf.ToLower().Contains(cpf.ToLower())) &&
            (string.IsNullOrEmpty(nis)  || fu.Usuario.Nis.ToLower().Contains(nis.ToLower()))
        ));

        return await query
            .Include(p => p.HistoricoCras) // ADICIONADO
                .ThenInclude(hc => hc.Cras)
            .Include(p => p.Cras)
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .AsNoTracking()
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetAllProntuariosByUsuarioSearch(Usuario usuario)
    {
        return await GetAllProntuariosByUsuarioSearch(usuario.Nome, usuario.Cpf, usuario.Nis);
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
