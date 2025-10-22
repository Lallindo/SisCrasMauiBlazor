using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class CrasRepository(SisCrasDbContext dbContext) : EfRepository<Cras>(dbContext), ICrasRepository
{
    public async Task<List<Tecnico>> ReturnTecnicosFromCras(int id)
    {
        var cras = await _DbContext.Cras
            .Include(c => c.TecnicosCras)
            .ThenInclude(tc => tc.Tecnico)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cras?.TecnicosCras
            .Select(tc => tc.Tecnico)
            .ToList() ?? [];
    }
}