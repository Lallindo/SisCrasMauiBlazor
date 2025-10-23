using SisCras.Models;

namespace SisCras.Repositories;

public interface IFamiliaRepository : IRepository<Familia>
{
    Task<List<Usuario>> GetActiveUsuariosFromFamilia(Familia familia);
    Task<List<Usuario>> GetActiveUsuariosFromFamilia(int id);
    Task<List<Usuario>> GetUsuariosFromFamilia(Familia familia);
    Task<List<Usuario>> GetUsuariosFromFamilia(int id);
}