using SisCras.Domain.Entities;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class CrasService(ICrasRepository crasRepository) : BaseService<Cras>(crasRepository), ICrasService
{
    private ICrasRepository CrasRepository { get; } = crasRepository;

    public async Task<List<Tecnico>> GetTecnicosFromCras(int id)
    {
        return await CrasRepository.GetTecnicosFromCras(id);
    }

    public async Task<List<Tecnico>> GetTecnicosFromCras(Cras cras)
    {
        return await CrasRepository.GetTecnicosFromCras(cras);
    }

    public async Task<List<Familia>> GetFamiliasFromCras(int id)
    {
        return await CrasRepository.GetFamiliasFromCras(id);
    }

    public async Task<List<Familia>> GetFamiliasFromCras(Cras cras)
    {
        return await CrasRepository.GetFamiliasFromCras(cras);
    }

    public async Task<List<Prontuario>> GetProntuariosFromCras(int id)
    {
        return await CrasRepository.GetProntuariosFromCras(id);
    }

    public async Task<List<Prontuario>> GetProntuariosFromCras(Cras cras)
    {
        return await CrasRepository.GetProntuariosFromCras(cras);;
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(int id)
    {
        return await CrasRepository.GetProntuarioAndFamiliaAndUsuariosFromCras(id);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(Cras cras)
    {
        return await CrasRepository.GetProntuarioAndFamiliaAndUsuariosFromCras(cras);
    }

    public async Task<List<Prontuario>> GetAllProntuariosAndFamiliaAndUsuarios(int offset = 0, int limit = 20)
    {
        return await CrasRepository.GetAllProntuariosAndFamiliaAndUsuarios(offset, limit);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(int id)
    {
        return await CrasRepository.GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(id);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(Cras cras)
    {
        return await CrasRepository.GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(cras);
    }
}
