using SisCras.Domain.Entities;
using SisCras.Domain.Entities.ValueObjects;

namespace SisCras.Infrastructure.Repositories;

public interface ITecnicoRepository : IRepository<Tecnico>
{
    Task<Tecnico?> GetTecnicoByLogin(string login);
    Task<CrasInfo?> GetCurrentCrasById(int id);
}