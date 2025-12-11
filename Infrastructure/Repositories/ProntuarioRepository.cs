using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class ProntuarioRepository(SisCrasDbContext dbContext)
    : BaseRepository<Prontuario>(dbContext), IProntuarioRepository
{
    // Método auxiliar para garantir que o histórico venha junto
    private IQueryable<Prontuario> GetQueryWithIncludes()
    {
        return DbContext.Prontuarios
            .Include(p => p.HistoricoCras)
                .ThenInclude(hc => hc.Cras)
            .Include(p => p.Familia)
                .ThenInclude(f => f.FamiliaUsuarios)
                .ThenInclude(fu => fu.Usuario)
            .Include(p => p.Cras)     // Carrega o CRAS do "cache"
            .Include(p => p.Tecnico); // Carrega o Tecnico do "cache"
    }

    public async Task<Prontuario?> GetFamiliaAndUsuariosFromProntuario(int id)
    {
        return await GetQueryWithIncludes()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Prontuario?> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario)
    {
        return await GetFamiliaAndUsuariosFromProntuario(prontuario.Id);
    }

    public async Task<Prontuario?> GetFamiliaFromProntuario(int id)
    {
        return await DbContext.Prontuarios
            .Include(p => p.HistoricoCras)
            .Include(p => p.Familia)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Prontuario?> GetFamiliaFromProntuario(Prontuario prontuario)
    {
        return await GetFamiliaFromProntuario(prontuario.Id);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(int familiaId)
    {
        // Pega o prontuário onde o histórico atual está aberto (DataSaida == null)
        // OU simplesmente pega pelo ID da família, e o código filtra depois.
        return await GetQueryWithIncludes()
            .Where(p => p.FamiliaId == familiaId)
            .FirstOrDefaultAsync(); 
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(Familia familia)
    {
        return await GetProntuarioByFamiliaId(familia.Id);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(int familiaId)
    {
        return await GetQueryWithIncludes()
            .Where(p => p.FamiliaId == familiaId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(Familia familia)
    {
        return await GetProntuarioByFamiliaIdNoTracking(familia.Id);
    }

    public async Task DeleteAsync(Prontuario obj, CancellationToken cancellationToken = default)
    {
        // Soft Delete: Encerra o histórico atual
        var currObj = await DbContext.Prontuarios
            .Include(p => p.HistoricoCras)
            .FirstOrDefaultAsync(p => p.Id == obj.Id, cancellationToken);
        
        if (currObj != null && currObj.ProntuarioAtivo != null)
        {
            currObj.ProntuarioAtivo.DataSaida = DateTime.Now;
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
