using SisCras.Domain.Entities;
using SisCras.Domain.Enums;
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

    public async Task<Prontuario?> ImportProntuario(Prontuario prontuarioParam)
    {
        if (prontuarioParam.Id == 0) return null;

        // 1. Busca o prontuário (O Repository já traz o HistoricoCras com Include)
        var prontuarioExistente = await ProntuarioRepository.GetProntuarioByFamiliaId(prontuarioParam.FamiliaId);

        if (prontuarioExistente == null) return null;

        var tecnicoLogado = LoggedUserService.GetCurrentUser();
        if (tecnicoLogado?.CrasAtivo == null) return null; // Validação simples

        // 2. Fecha o histórico anterior
        var vinculoAnterior = prontuarioExistente.ProntuarioAtivo;
        if (vinculoAnterior != null)
        {
            // Se já está no mesmo CRAS, não faz nada
            if (vinculoAnterior.CrasId == tecnicoLogado.CrasAtivo.Id) 
                return prontuarioExistente;

            vinculoAnterior.DataSaida = DateTime.Now;
        }

        // 3. Adiciona o novo histórico (Entrada)
        var novoHistorico = new ProntuarioCras
        {
            // ProntuarioId será preenchido automaticamente ao adicionar na lista
            CrasId = tecnicoLogado.CrasAtivo.Id,
            TecnicoResponsavelId = tecnicoLogado.Id,
            DataEntrada = DateTime.Now,
            DataSaida = null,
            FormaDeAcesso = vinculoAnterior?.FormaDeAcesso ?? FormaAcessoEnum.Espontanea
        };
        
        prontuarioExistente.HistoricoCras.Add(novoHistorico);

        // 4. Atualiza o "Cache" no pai para compatibilidade com telas antigas
        prontuarioExistente.CrasId = tecnicoLogado.CrasAtivo.Id;
        prontuarioExistente.TecnicoId = tecnicoLogado.Id;

        // 5. Salva (O EF identifica as mudanças na lista e na entidade pai)
        await ProntuarioRepository.UpdateAsync(prontuarioExistente);

        return prontuarioExistente;
    }
    
    // ApplicationLayer/Services/ProntuarioService.cs

    public async Task<Prontuario?> ReactivateProntuario(int prontuarioId)
    {
        // 1. Carrega o prontuário com o histórico (o repositório deve ter o include)
        var prontuarioExistente = await ProntuarioRepository.GetByIdAsync(prontuarioId);

        if (prontuarioExistente == null)
            throw new KeyNotFoundException($"Prontuário com ID {prontuarioId} não encontrado.");

        // 2. Valida o estado atual: se já tiver um ativo, impede a reativação
        if (prontuarioExistente.ProntuarioAtivo != null)
        {
            throw new InvalidOperationException("O prontuário já está ativo no CRAS " +
                                                $"{prontuarioExistente.ProntuarioAtivo.Cras.Nome} e não pode ser reativado.");
        }
    
        // 3. Valida o Técnico Logado e o CRAS de destino
        var tecnicoLogado = LoggedUserService.GetCurrentUser();
        if (tecnicoLogado?.CrasAtivo == null)
            throw new InvalidOperationException("Técnico sem CRAS ativo. É necessário um CRAS de destino para reativação.");

        // 4. Cria o NOVO registro de histórico (ProntuarioCras ATIVO)
        var novoVinculo = new ProntuarioCras
        {
            ProntuarioId = prontuarioExistente.Id,
            CrasId = tecnicoLogado.CrasAtivo.Id,
            TecnicoResponsavelId = tecnicoLogado.Id,
            DataEntrada = DateTime.Now,
            DataSaida = null, // Ativo
        
            // Forma de Acesso: Usamos o tipo 'EncaminhamentoOutro' (ou outro específico, se existir)
            FormaDeAcesso = FormaAcessoEnum.EncaminhamentoOutro 
        };

        // 5. Adiciona o novo vínculo à lista
        prontuarioExistente.HistoricoCras.Add(novoVinculo);

        // 6. Atualiza o cache do Prontuário principal
        prontuarioExistente.CrasId = tecnicoLogado.CrasAtivo.Id;
        prontuarioExistente.TecnicoId = tecnicoLogado.Id;

        // 7. Salva as alterações
        await ProntuarioRepository.UpdateAsync(prontuarioExistente);

        return prontuarioExistente;
    }
}
