using SisCras.Domain.Entities;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class ProntuarioService(
    IProntuarioRepository prontuarioRepository,
    ILoggedUserService loggedUserService)
    : BaseService<Prontuario>(prontuarioRepository), IProntuarioService
{
    private IProntuarioRepository ProntuarioRepository { get; } = prontuarioRepository;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;

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

    public async Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(int familiaId)
    {
        return await ProntuarioRepository.GetProntuarioByFamiliaIdNoTracking(familiaId);
    }

    public async Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(Familia familia)
    {
        return await ProntuarioRepository.GetProntuarioByFamiliaIdNoTracking(familia);
    }

    public async Task<Prontuario?> ImportProntuario(Prontuario prontuario)
    {
        if (prontuario.Id == 0) return null;
        
        var resp = await ProntuarioRepository.GetByIdAsync(prontuario.Id);

        if (resp == null) return null;

        var familiaId = resp.FamiliaId; 
        var formaAcesso = resp.FormaDeAcesso;

        await ProntuarioRepository.DeleteAsync(resp);

        var tecnicoLogado = LoggedUserService.GetCurrentUser();

        if (tecnicoLogado.CrasAtivo == null)
        {
            throw new InvalidOperationException("O técnico logado não possui um CRAS ativo para realizar a importação.");
        }

        Prontuario novoProntuario = new()
        {
            CrasId = tecnicoLogado.CrasAtivo.Id, 
            FamiliaId = familiaId,               
            TecnicoId = tecnicoLogado.Id,        
            DataCriacao = DateOnly.FromDateTime(DateTime.Now),
            FormaDeAcesso = formaAcesso
        };
        
        await ProntuarioRepository.AddAsync(novoProntuario);

        return novoProntuario;
    }
}
