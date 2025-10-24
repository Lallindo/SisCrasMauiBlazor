using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class UsuarioRepository(SisCrasDbContext dbContext) : EfRepository<Usuario>(dbContext), IUsuarioRepository
{
    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await DbContext.Familias
            .Include(f => f.Prontuarios)
            .Where(f => f.FamiliaUsuarios.Any(fu => fu.UsuarioId == id && fu.Ativo))
            .FirstOrDefaultAsync();
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario)
    {
        return await GetActiveFamiliaFromUsuario(usuario.Id);
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