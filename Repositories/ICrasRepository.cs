using SisCras.Models;

namespace SisCras.Repositories;

public interface ICrasRepository : IRepository<Cras>
{
    Task<List<Tecnico>> ReturnTecnicosFromCras(int id); 
}