using SisCras.Models;

namespace SisCras.Services;

public interface IFamiliaService : IBaseService<Familia>
{
    Task<List<Usuario>> GetActiveUsuariosFromFamilia(Familia familia);
    Task<List<Usuario>> GetActiveUsuariosFromFamilia(int id);
    Task<List<Usuario>> GetUsuariosFromFamilia(Familia familia);
    Task<List<Usuario>> GetUsuariosFromFamilia(int id);
    Task<Usuario?> GetResponsavelFromFamilia(int id);
    Task<Usuario?> GetResponsavelFromFamilia(Familia familia);
}