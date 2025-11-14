using SisCras.Domain.Entities;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class ProntuarioService(IProntuarioRepository prontuarioRepository)
    : BaseService<Prontuario>(prontuarioRepository), IProntuarioService
{
    private IProntuarioRepository ProntuarioRepository { get; } = prontuarioRepository;

    public async Task<Prontuario> GetFamiliaFromProntuario(Prontuario prontuario)
    {
        return await ProntuarioRepository.GetFamiliaFromProntuario(prontuario);
    }

    public async Task<Prontuario> GetFamiliaFromProntuario(int id)
    {
        return await ProntuarioRepository.GetFamiliaFromProntuario(id);
    }

    public async Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario)
    {
        return await ProntuarioRepository.GetFamiliaAndUsuariosFromProntuario(prontuario);
    }

    public async Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(int id)
    {
        return await ProntuarioRepository.GetFamiliaAndUsuariosFromProntuario(id);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(int familiaId)
    {
        return await ProntuarioRepository.GetProntuarioByFamiliaId(familiaId);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaId(Familia familia)
    {
        return await ProntuarioRepository.GetProntuarioByFamiliaId(familia);
    }
}