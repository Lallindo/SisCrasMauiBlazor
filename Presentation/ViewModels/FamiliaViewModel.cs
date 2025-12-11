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
    [ObservableProperty] private bool _showDeactivated = false; // Valor do Checkbox

    public ObservableCollection<Prontuario> Prontuarios
    {
        get
        {
            // O filtro é aplicado AQUI, com base no valor do _showDeactivated (checkbox)
            if (_showDeactivated) // Se o checkbox estiver marcado, retorna todos
            {
                return _prontuarios;
            }
            else
            {
                // Se o checkbox NÃO estiver marcado, filtra para retornar apenas os ativos
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
        {
            // FIX: Carrega TODOS os prontuários que correspondem à busca, ativos ou inativos.
            // O filtro visual é aplicado no getter da propriedade Prontuarios.
            var searchResults = await _usuarioService.GetAllProntuariosByUsuarioSearch(SearchUsuario);
            _prontuarios = new ObservableCollection<Prontuario>(searchResults);
        }
        else
            await GetAllProntuarios();
            
        // Notifica a UI para que a propriedade computada 'Prontuarios' seja reavaliada.
        OnPropertyChanged(nameof(Prontuarios));
    }

    public async Task GetAllProntuarios()
    {
        // FIX: Remove o filtro desnecessário aqui. Carrega TODOS os prontuários.
        var todosProntuarios =
            await _crasService.GetAllProntuariosAndFamiliaAndUsuarios();
    
        // A coleção interna deve conter TUDO.
        _prontuarios =
            new ObservableCollection<Prontuario>(todosProntuarios); 
        
        // Notifica a UI para que a propriedade computada 'Prontuarios' seja reavaliada.
        OnPropertyChanged(nameof(Prontuarios));
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

        // O Repositório encerra o vínculo ativo no ProntuarioCras
        await _prontuarioService.DeleteAsync(prontuario);
    
        // Atualiza a lista na tela
        await GetAllProntuarios();
    }

    [RelayCommand]
    private async Task ImportProntuario(Prontuario prontuario)
    {
        var confirm = await Application.Current.MainPage.DisplayAlert("Realizar transferência?", "Deseja mesmo realizar a transferência?", "Sim", "Não");
        if (!confirm) return;
        // Apenas chama o serviço. Toda a lógica complexa de fechar histórico antigo e abrir novo está lá.
        var novoProntuario = await _prontuarioService.ImportProntuario(prontuario);
    
        if (novoProntuario != null)
        {
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Família transferida para seu CRAS com sucesso!", "OK");
            await GetAllProntuarios();
        }
    }
    
    partial void OnShowDeactivatedChanged(bool value)
    {
        OnPropertyChanged(nameof(Prontuarios));
    }
    
    [RelayCommand]
    private async Task ReactivateProntuario(Prontuario prontuario)
    {
        var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", 
            $"Deseja reativar o prontuário {prontuario.Id} no seu CRAS?", "Sim", "Não");
        if (!confirm) return;

        try
        {
            await _prontuarioService.ReactivateProntuario(prontuario.Id);
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Prontuário reativado com sucesso!", "OK");
            await GetAllProntuarios();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
