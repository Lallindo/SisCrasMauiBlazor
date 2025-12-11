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
    private Prontuario? _prontuarioParaRemover;
    [ObservableProperty] private ObservableCollection<Prontuario?> _prontuarios = [];
    [ObservableProperty] private Tecnico? _loggedTecnico = new();
    [ObservableProperty] private Usuario _searchUsuario = new();

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
            Prontuarios =
                new ObservableCollection<Prontuario?>(
                    await _usuarioService.GetAllProntuariosByUsuarioSearch(SearchUsuario));
        else
            await GetAllProntuarios();
    }

    public async Task GetAllProntuarios()
    {
        Prontuarios =
            new ObservableCollection<Prontuario>(await _crasService.GetAllProntuariosAndFamiliaAndUsuarios());
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
        await _prontuarioService.DeleteAsync(prontuario);
    }
}
