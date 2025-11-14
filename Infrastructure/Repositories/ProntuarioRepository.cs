using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class ProntuarioRepository(SisCrasDbContext dbContext)
    : BaseRepository<Prontuario>(dbContext), IProntuarioRepository
{
    public async Task<Prontuario?> GetFamiliaAndUsuariosFromProntuario(int id)
    {
        return await DbContext.Prontuarios
            .Include(p => p.Cras)
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Prontuario?> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario)
    {
        return await GetFamiliaAndUsuariosFromProntuario(prontuario.Id);
    }

    public async Task<Prontuario?> GetFamiliaFromProntuario(int id)
    {
        return await DbContext.Prontuarios
            .Include(p => p.Familia)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Prontuario?> GetFamiliaFromProntuario(Prontuario prontuario)
    {
        return await GetFamiliaFromProntuario(prontuario.Id);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(int familiaId)
    {
        return await DbContext.Prontuarios
            .Where(p => p.FamiliaId == familiaId && p.DataSaida == null)
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .FirstOrDefaultAsync();
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(Familia familia)
    {
        return await GetProntuarioByFamiliaId(familia.Id);
    }
}