using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;
using SisCras.Models.ValueObjects;

namespace SisCras.Repositories;

public class TecnicoRepository(SisCrasDbContext dbContext) : EfRepository<Tecnico>(dbContext), ITecnicoRepository
{
    public async Task<Tecnico> GetTecnicoByLoginAsync(string login)
    {
        return await _DbContext.Tecnicos.FirstOrDefaultAsync(t => t.Login == login);
    }

    public async Task<CrasInfo?> GetCurrentCrasByIdAsync(int id)
    {
        return await _DbContext.TecnicoCras
            .Where(tc => tc.TecnicoId == id && tc.DataSaida == null)
            .Select(tc => new CrasInfo(tc.Cras.Id, tc.Cras.Nome))
            .FirstOrDefaultAsync();
    }
}