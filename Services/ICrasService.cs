using SisCras.Models;

namespace SisCras.Services;

public interface ICrasService : IBaseService<Cras>
{
    Task<List<Tecnico>> GetAllTecnicos(int id);
}