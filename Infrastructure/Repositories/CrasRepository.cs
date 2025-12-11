using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class CrasRepository(SisCrasDbContext dbContext) : BaseRepository<Cras>(dbContext), ICrasRepository
{
    public async Task<List<Familia>> GetFamiliasFromCras(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.Prontuarios)
            .Select(p => p.Familia)
            .ToListAsync();
    }

    public async Task<List<Familia>> GetFamiliasFromCras(Cras cras)
    {
        return await GetFamiliasFromCras(cras.Id);
    }

    public async Task<List<Prontuario>> GetProntuariosFromCras(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.Prontuarios)
            .Include(p => p.HistoricoCras) // ADICIONADO: Necessário para lógica de histórico
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetProntuariosFromCras(Cras cras)
    {
        return await GetProntuariosFromCras(cras.Id);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.Prontuarios)
            .Include(p => p.HistoricoCras) // ADICIONADO
                .ThenInclude(hc => hc.Cras) // Opcional: carrega o CRAS do histórico
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(Cras cras)
    {
        return await GetProntuarioAndFamiliaAndUsuariosFromCras(cras.Id);
    }

    public async Task<List<Prontuario>> GetAllProntuariosAndFamiliaAndUsuarios(int offset = 0, int limit = 20)
    {
        return await DbContext.Prontuarios
            .Include(p => p.HistoricoCras) // ADICIONADO
                .ThenInclude(hc => hc.Cras)
            .Include(p => p.Cras)
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.Prontuarios)
            .Include(p => p.HistoricoCras) // ADICIONADO
                .ThenInclude(hc => hc.Cras)
            .Include(p => p.Cras)
            .Include(p => p.Familia)
            .ThenInclude(f => f.FamiliaUsuarios)
            .ThenInclude(fu => fu.Usuario)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(Cras cras)
    {
        return await GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(cras.Id);
    }

    public async Task<List<Tecnico>> GetTecnicosFromCras(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.TecnicosCras)
            .Where(tc => tc.DataSaida == null)
            .Select(tc => tc.Tecnico)
            .ToListAsync();
    }

    public async Task<List<Tecnico>> GetTecnicosFromCras(Cras cras)
    {
        return await GetTecnicosFromCras(cras.Id);
    }
}
