using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class FamiliaViewModel(
    IFamiliaService familiaService,
    IUsuarioService usuarioService,
    ICrasService crasService, 
    IProntuarioService prontuarioService,
    ILoggedUserService loggedUserService,
    NavigationManager navigationManager) : BaseViewModel
{
    [ObservableProperty] private Tecnico? _loggedTecnico = new();
    [ObservableProperty] private Usuario _searchUsuario = new();
    [ObservableProperty] private bool _showDeactivated = false;

    public ObservableCollection<Prontuario> Prontuarios
    {
        get
        {
            if (ShowDeactivated)
            {
                return _prontuarios;
            }
            else
            {
                return new(from p in _prontuarios where p.ProntuarioAtivo != null select p);
            }
        }
    }

    private ObservableCollection<Prontuario> _prontuarios = [];
    
    private IFamiliaService _familiaService { get; } = familiaService;
    private IUsuarioService _usuarioService { get; } = usuarioService;
    private ICrasService _crasService { get; } = crasService;
    private IProntuarioService _prontuarioService { get; } = prontuarioService;
    private ILoggedUserService _loggedUserService { get; } = loggedUserService;
    private NavigationManager _navigationManager { get; } = navigationManager;

    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchUsuario.Nome) ||
                                  !string.IsNullOrEmpty(SearchUsuario.Cpf) || !string.IsNullOrEmpty(SearchUsuario.Nis);

    [RelayCommand]
    private Task GoToRegistrarFamilia()
    {
        _navigationManager.NavigateTo(
            $"/familias/novo?nome={SearchUsuario.Nome}&cpf={SearchUsuario.Cpf}&nis={SearchUsuario.Nis}");
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task SearchFamiliasByUsuario()
    {
        if (HasSearchTerm)
            _prontuarios =
                new ObservableCollection<Prontuario>(
                    await _usuarioService.GetAllProntuariosByUsuarioSearch(SearchUsuario));
        else
            await GetAllProntuarios();
    }

    public async Task GetAllProntuarios()
    {
        // O CrasService deve retornar TODOS os prontuários vinculados a esse CRAS (ativos e inativos)
        // Se o seu CrasRepository já filtra por DataSaida == null no ProntuarioCras, ignore o .Where abaixo
        // Mas, para segurança, filtramos aqui pelo ProntuarioAtivo
        var todosProntuarios =
            await _crasService.GetAllProntuariosAndFamiliaAndUsuarios();
    
        // FILTRO ESSENCIAL: Garante que apenas os prontuários que possuem um vínculo ativo (ProntuarioAtivo != null) sejam exibidos
        // Isso usa a propriedade calculada ProntuarioAtivo na sua entidade Prontuario.
        _prontuarios =
            new ObservableCollection<Prontuario>(todosProntuarios.Where(p => p.ProntuarioAtivo != null)); 
    }

    public async Task GetLoggedUsuario()
    {
        LoggedTecnico = _loggedUserService.GetCurrentUser();
    }

    [RelayCommand]
    private async Task GoToEditarFamilia(int familiaId)
    {
        _navigationManager.NavigateTo($"/familias/editar/{familiaId}");
    }
    
    [RelayCommand]
    private async Task GoToVisualizarFamilia(int familiaId)
    {
        _navigationManager.NavigateTo($"/familias/visualizar/{familiaId}");
    }
    
    [RelayCommand]
    private async Task DeactivateProntuario(Prontuario prontuario)
    {
        var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Deseja realmente desativar este prontuário?", "Sim", "Não");
        if (!confirm) return;

        // O Repositório agora sabe que "Delete" significa "Encerrar Histórico Atual"
        await _prontuarioService.DeleteAsync(prontuario);
    
        // Atualiza a lista na tela
        await GetAllProntuarios();
    }

    [RelayCommand]
    private async Task ImportProntuario(Prontuario prontuario)
    {
        // Apenas chama o serviço. Toda a lógica complexa de fechar histórico antigo e abrir novo está lá.
        var novoProntuario = await _prontuarioService.ImportProntuario(prontuario);
    
        if (novoProntuario != null)
        {
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Família transferida para seu CRAS com sucesso!", "OK");
            await GetAllProntuarios();
        }
    }
}
