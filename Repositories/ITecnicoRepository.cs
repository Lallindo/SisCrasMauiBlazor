using SisCras.Models;
using SisCras.Models.ValueObjects;

namespace SisCras.Repositories;

public interface ITecnicoRepository : IRepository<Tecnico>
{
    Task<Tecnico> GetTecnicoByLogin(string login);
    Task<CrasInfo?> GetCurrentCrasById(int id);
}