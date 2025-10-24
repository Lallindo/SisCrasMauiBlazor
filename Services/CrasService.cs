using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

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
        return await CrasRepository.GetProntuariosFromCras(cras);
    }
}