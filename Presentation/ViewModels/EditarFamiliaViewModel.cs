using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Havit.Blazor.Components.Web.Bootstrap;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

namespace SisCras.Presentation.ViewModels;

public partial class EditarFamiliaViewModel(IFamiliaService familiaService, IProntuarioService prontuarioService, IUsuarioService usuarioService) : BaseViewModel
{
    private IProntuarioService _ProntuarioService = prontuarioService;
    private IFamiliaService _FamiliaService = familiaService;
    private IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty] private Prontuario? _selectedProntuario = new();
    [ObservableProperty] private bool _showUnactiveUsuarios = false; 
    private List<int> VinculosParaEncerrar = [];
    private Usuario? UsuarioBeingEdited = null;
    public HxModal DuplicateModal;
    [ObservableProperty] private FamiliaUsuario? _usuarioForModal = null;
    public ObservableCollection<FamiliaUsuario> Usuarios
    {
        get
        {
            if (!ShowUnactiveUsuarios)
            {
                return new(from fu in SelectedProntuario.Familia.FamiliaUsuarios where fu.Ativo select fu);
            }
            return new((from fu in SelectedProntuario.Familia.FamiliaUsuarios select fu).OrderBy(fu => fu.Ativo ? 0 : 1));
        }
    }

    public async Task GetProntuario(int familiaId)
    {
        SelectedProntuario = await _ProntuarioService.GetProntuarioByFamiliaIdNoTracking(familiaId);
    }

    [RelayCommand]
    public async Task UpdateProntuario()
    {
        await _ProntuarioService.UpdateAsync(SelectedProntuario);
    }

    [RelayCommand]
    private async Task DeactivateUsuario(FamiliaUsuario usuario)
    {
        await SelectedProntuario.Familia.ToggleAtivoUsuario(usuario);
    }
    
    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        var usuarioCount = SelectedProntuario.Familia.FamiliaUsuarios.Count;
        SelectedProntuario.Familia.FamiliaUsuarios.Add(new FamiliaUsuario
        {
            Usuario = new Usuario(),
            Parentesco = usuarioCount == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Conjuge,
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
    private async Task ImportUsuarioData()
    {
        if (UsuarioForModal?.Usuario == null || UsuarioBeingEdited == null) return;
        
        VinculosParaEncerrar.Add(UsuarioForModal.Usuario.Id);

        var vinculoAtualNoFormulario = SelectedProntuario.Familia.FamiliaUsuarios
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
