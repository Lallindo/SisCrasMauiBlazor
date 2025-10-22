using SisCras.Models;
using SisCras.Models.ValueObjects;

namespace SisCras.Repositories;

public interface ITecnicoRepository : IRepository<Tecnico>
{
    Task<Tecnico> GetTecnicoByLoginAsync(string login);
    Task<CrasInfo?> GetCurrentCrasByIdAsync(int id);
}