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
        return await GetTecnicosFromCras(cras.Id);
    }
    public async Task<List<Familia>> GetFamiliasFromCras(int id)
    {
        return await CrasRepository.GetFamiliasFromCras(id);
    }
    public async Task<List<Familia>> GetFamiliasFromCras(Cras cras)
    {
        return await GetFamiliasFromCras(cras.Id);
    }
    public async Task<List<Prontuario>> GetProntuariosFromCras(int id)
    {
        return await CrasRepository.GetProntuariosFromCras(id);
    }
    public async Task<List<Prontuario>> GetProntuariosFromCras(Cras cras)
    {
        return await GetProntuariosFromCras(cras.Id);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(int id)
    {
        return await CrasRepository.GetProntuarioAndFamiliaAndUsuariosFromCras(id);
    }

    public async Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(Cras cras)
    {
        return await GetProntuarioAndFamiliaAndUsuariosFromCras(cras.Id);
    }
}