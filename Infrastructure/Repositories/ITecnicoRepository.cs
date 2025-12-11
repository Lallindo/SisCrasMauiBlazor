using SisCras.Domain.Entities;
using SisCras.Domain.ValueObjects;

namespace SisCras.Infrastructure.Repositories;

public interface ITecnicoRepository : IRepository<Tecnico>
{
    Task<Tecnico?> GetTecnicoByLogin(string login);
    Task<Cras?> GetCurrentCrasById(int id);
    Task<List<Tecnico>> GetAllTecnicos();
}
