using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class FamiliaViewModel(IFamiliaService familiaService, IUsuarioService usuarioService, ICrasService crasService, ILoggedUserService loggedUserService, NavigationManager navigationManager) : BaseViewModel
{
    private Prontuario? _prontuarioParaRemover;
    [ObservableProperty]
    private ObservableCollection<Prontuario> _prontuarios = [];
    [ObservableProperty]
    private Tecnico? _loggedTecnico = new();
    [ObservableProperty]
    private Usuario _searchUsuario = new();

    private IFamiliaService _familiaService { get; } = familiaService;
    private IUsuarioService _usuarioService { get; } = usuarioService;
    private ILoggedUserService _loggedUserService { get; } = loggedUserService;
    private ICrasService _crasService { get; } = crasService;
    private NavigationManager _navigationManager { get; } = navigationManager;
    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchUsuario.Nome) || !string.IsNullOrEmpty(SearchUsuario.Cpf) || !string.IsNullOrEmpty(SearchUsuario.Nis);
    
    [RelayCommand]
    private async Task GoToRegistrarFamilia()
    {
        _navigationManager.NavigateTo("/familias/buscar");
    }

    [RelayCommand]
    public async Task SearchFamiliasByUsuario()
    {
        if (HasSearchTerm)
        {
            Prontuarios = new(await _usuarioService.GetAllProntuariosByUsuarioSearch(SearchUsuario));
        }
        else
        {
            await GetAllProntuarios();
        }
    }
    public async Task GetAllProntuarios()
    {
        Prontuarios = new(await _crasService.GetProntuarioAndFamiliaAndUsuariosFromCras(1));
    }
    [RelayCommand]
    private async Task GoToEditarFamilia(int familiaId)
    {
        _navigationManager.NavigateTo($"/familias/editar/{familiaId}");
    }
}