using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class CrasService(ICrasRepository crasRepository) : BaseService<Cras>(crasRepository), ICrasService
{
    ICrasRepository _CrasRepository { get; } = crasRepository;

    public async Task<List<Tecnico>> GetAllTecnicos(int id)
    {
        return await _CrasRepository.GetTecnicosFromCras(id);
    }
}