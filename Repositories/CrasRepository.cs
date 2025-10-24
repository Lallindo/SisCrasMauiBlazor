using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class CrasRepository(SisCrasDbContext dbContext) : EfRepository<Cras>(dbContext), ICrasRepository
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
            .ToListAsync();
    }

    public async Task<List<Prontuario>> GetProntuariosFromCras(Cras cras)
    {
        return await GetProntuariosFromCras(cras.Id);
    }

    public async Task<List<Tecnico>> GetTecnicosFromCras(int id)
    {
        return await DbContext.Cras
            .Where(c => c.Id == id)
            .SelectMany(c => c.TecnicosCras)
            .Select(tc => tc.Tecnico)
            .ToListAsync();
    }

    public async Task<List<Tecnico>> GetTecnicosFromCras(Cras cras)
    {
        return await GetTecnicosFromCras(cras.Id); 
    }
}