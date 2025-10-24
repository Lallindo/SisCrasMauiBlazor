using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class FamiliaService(IFamiliaRepository familiaRepository) : BaseService<Familia>(familiaRepository), IFamiliaService
{
    private IFamiliaRepository FamiliaRepository { get; } = familiaRepository;
    public async Task<List<Usuario>> GetActiveUsuariosFromFamilia(Familia familia)
    {
        return await FamiliaRepository.GetActiveUsuariosFromFamilia(familia);
    }
    public async Task<List<Usuario>> GetActiveUsuariosFromFamilia(int id)
    {
        return await FamiliaRepository.GetActiveUsuariosFromFamilia(id);
    }
    public async Task<List<Usuario>> GetUsuariosFromFamilia(Familia familia)
    {
        return await FamiliaRepository.GetUsuariosFromFamilia(familia);
    }
    public async Task<List<Usuario>> GetUsuariosFromFamilia(int id)
    {
        return await FamiliaRepository.GetUsuariosFromFamilia(id);
    }
    public async Task<Usuario?> GetResponsavelFromFamilia(int id)
    {
        return await FamiliaRepository.GetResponsavelFromFamilia(id);
    }
    public async Task<Usuario?> GetResponsavelFromFamilia(Familia familia)
    {
        return await FamiliaRepository.GetResponsavelFromFamilia(familia);
    }
}