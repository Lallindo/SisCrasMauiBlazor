using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;
using SisCras.Domain.ValueObjects;

namespace SisCras.Infrastructure.Repositories;

public class TecnicoRepository(SisCrasDbContext dbContext) : BaseRepository<Tecnico>(dbContext), ITecnicoRepository
{
    public async Task<Tecnico?> GetTecnicoByLogin(string login)
    {
        return await DbContext.Tecnicos
            .Include(
                t => t.TecnicoCras.Where(tc => tc.DataSaida == null)
                )
            .ThenInclude(tc => tc.Cras)
            .FirstOrDefaultAsync(t => t.Login == login);
    }

    public async Task<Cras?> GetCurrentCrasById(int id)
    {
         return await DbContext.TecnicoCras
            .Where(tc => tc.TecnicoId == id && tc.DataSaida == null)
            .Select(tc => tc.Cras)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(Tecnico obj, CancellationToken cancellationToken = default)
    {
        obj.TecnicoCrasAtivo.DataSaida = DateOnly.FromDateTime(DateTime.Now);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
