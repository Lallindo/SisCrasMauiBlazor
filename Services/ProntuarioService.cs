using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class ProntuarioService(IProntuarioRepository prontuarioRepository) : BaseService<Prontuario>(prontuarioRepository), IProntuarioService
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
}