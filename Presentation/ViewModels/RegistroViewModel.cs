using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Havit.Blazor.Components.Web.Bootstrap;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

namespace SisCras.Presentation.ViewModels;

public partial class RegistroViewModel(
    IProntuarioService prontuarioService,
    IFamiliaService familiaService,
    ILoggedUserService loggedUserService,
    IUsuarioService usuarioService
) : BaseViewModel
{
    private readonly IFamiliaService _familiaService = familiaService;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;
    private readonly IProntuarioService _prontuarioService = prontuarioService;
    private readonly IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty] private Prontuario _prontuario = new()
    {
        Familia = new()
        {
            FamiliaUsuarios = []
        }
    };
    [ObservableProperty] private Usuario? _usuarioByQuery = new();
    [ObservableProperty] private ObservableCollection<Prontuario> _prontuariosEncontrados = [];
    private List<int> VinculosParaEncerrar = [];
    private Usuario? UsuarioBeingEdited = null;
    public HxModal DuplicateModal;
    [ObservableProperty] private FamiliaUsuario? _usuarioForModal = null;

    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        var usuarioCount = Prontuario.Familia.FamiliaUsuarios.Count;
        Prontuario.Familia.FamiliaUsuarios.Add(new FamiliaUsuario
        {
            Usuario = new Usuario
            {
                Nome = usuarioCount == 1 ? UsuarioByQuery.Nome : "",
                Cpf = usuarioCount == 1 ? UsuarioByQuery.Cpf : "",
                Nis = usuarioCount == 1 ? UsuarioByQuery.Nis : ""
            },
            Parentesco = usuarioCount == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Default, 
            DataAdicao = DateOnly.FromDateTime(DateTime.Now.AddDays(-3))
        });
    }

    [RelayCommand]
    private async Task VerifyDuplicate(Usuario usuarioToVerify)
    {
        if (usuarioToVerify is null) return;

        UsuarioBeingEdited = usuarioToVerify;
        
        bool temDados = !string.IsNullOrWhiteSpace(usuarioToVerify.Nome) || 
                        !string.IsNullOrWhiteSpace(usuarioToVerify.Cpf) || 
                        !string.IsNullOrWhiteSpace(usuarioToVerify.Nis);

        if (!temDados) return;

        try
        {
            var resultado = await _usuarioService.GetUsuarioByUsuarioSearch(
                usuarioToVerify.Nome,
                usuarioToVerify.Cpf,
                usuarioToVerify.Nis
            );
            
            if (resultado != null)
            {
                foreach (var f in resultado.FamiliaUsuarios)
                {
                    if (f.DataSaida == null)
                    {
                        UsuarioForModal = f;
                        await DuplicateModal.ShowAsync();
                    }
                }
            }
            else
            {
                Debug.WriteLine("Nada encontrado");
            }
        } catch (Exception ex)
        {
            Debug.WriteLine($"Erro na busca: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task InsertNewProntuario()
    {
        var tecnicoLogado = _loggedUserService.GetCurrentUser();

        if (tecnicoLogado?.CrasAtivo == null)
        {
            Debug.WriteLine("Erro: Técnico não logado ou sem CRAS ativo.");
            return;
        }
    
        try
        {
            // 1. Encerra vínculos antigos de usuários importados
            foreach (var idVinculoAntigo in VinculosParaEncerrar)
            {
                await _usuarioService.DeactivateActiveFamiliaUsuario(idVinculoAntigo);
            }

            // 2. Preenche os IDs de "Cache" no Prontuario Pai (para listagem rápida)
            Prontuario.CrasId = tecnicoLogado.CrasAtivo.Id;
            Prontuario.TecnicoId = tecnicoLogado.Id;
        
            // 3. CRIA O PRIMEIRO REGISTRO DE HISTÓRICO (ProntuarioCras ATIVO)
            var primeiroVinculo = new ProntuarioCras
            {
                CrasId = tecnicoLogado.CrasAtivo.Id,
                TecnicoResponsavelId = tecnicoLogado.Id,
                DataEntrada = DateTime.Now,
                DataSaida = null, // Ativo
                FormaDeAcesso = Prontuario.FormaDeAcesso 
            };
        
            // Adiciona o primeiro vínculo à lista (o EF preencherá o ProntuarioId automaticamente)
            Prontuario.HistoricoCras.Add(primeiroVinculo);
        
            // 4. Garante que o ID seja 0 para ser um novo registro
            Prontuario.Id = 0; 

            // 5. Salva o Prontuário Pai, o que salva a Família e o novo ProntuarioCras em cascata.
            await _prontuarioService.AddAsync(Prontuario); 
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao salvar: {ex.Message}");
        }
        
        await Application.Current.MainPage.DisplayAlert("Cadastro realizado", 
            $"O prontuário {Prontuario.Id} foi cadastrado com sucesso", "Ok");
    }

    [RelayCommand]
    private async Task ImportUsuarioData()
    {
        if (UsuarioForModal?.Usuario == null || UsuarioBeingEdited == null) return;
        
        VinculosParaEncerrar.Add(UsuarioForModal.Usuario.Id);

        var vinculoAtualNoFormulario = Prontuario.Familia.FamiliaUsuarios
            .FirstOrDefault(fu => fu.Usuario == UsuarioBeingEdited);

        if (vinculoAtualNoFormulario != null)
        {
            vinculoAtualNoFormulario.Usuario = UsuarioForModal.Usuario;
            vinculoAtualNoFormulario.UsuarioId = UsuarioForModal.Usuario.Id;
        }

        await DuplicateModal.HideAsync();
        Debug.WriteLine($"Usuário {UsuarioForModal.Usuario.Nome} importado para o cadastro");
    }
}
