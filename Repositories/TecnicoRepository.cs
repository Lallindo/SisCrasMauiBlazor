using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;
using SisCras.Models.ValueObjects;

namespace SisCras.Repositories;

public class TecnicoRepository(SisCrasDbContext dbContext) : EfRepository<Tecnico>(dbContext), ITecnicoRepository
{
    public async Task<Tecnico?> GetTecnicoByLogin(string login)
    {
        return await DbContext.Tecnicos
            .FirstOrDefaultAsync(t => t.Login == login);
    }

    public async Task<CrasInfo?> GetCurrentCrasById(int id)
    {
        return await DbContext.TecnicoCras
            .Where(tc => tc.TecnicoId == id && tc.DataSaida == null)
            .Select(tc => new CrasInfo(tc.Cras.Id, tc.Cras.Nome))
            .FirstOrDefaultAsync();
    }
}