using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class FamiliaViewModel(IFamiliaService familiaService, ICrasService crasService, ILoggedUserService loggedUserService, NavigationManager navigationManager) : BaseViewModel
{
    private Prontuario? _prontuarioParaRemover;
    [ObservableProperty]
    private ObservableCollection<Prontuario> _prontuarios = [];
    [ObservableProperty]
    private Tecnico? _loggedTecnico = new();
    [ObservableProperty]
    private string _searchTerm = string.Empty;

    private IFamiliaService FamiliaService { get; } = familiaService;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    private ICrasService CrasService { get; } = crasService;
    private NavigationManager NavigationManager { get; } = navigationManager;
    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);
    
    [RelayCommand]
    private async Task GoToRegistrarFamilia()
    {
        NavigationManager.NavigateTo("/familias/buscar");
    }
    public async Task GetAllProntuarios()
    {
        Prontuarios = new(await CrasService.GetProntuarioAndFamiliaAndUsuariosFromCras(1));
    }
    [RelayCommand]
    private async Task GoToEditarFamilia(int familiaId)
    {
        NavigationManager.NavigateTo($"/familias/editar/{familiaId}");
    }
}