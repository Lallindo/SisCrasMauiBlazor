using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class FamiliaViewModel : BaseViewModel
{
    private Prontuario? _prontuarioParaRemover;
    [ObservableProperty]
    private ObservableCollection<Prontuario> _prontuarios = [];
    [ObservableProperty]
    private Tecnico? _loggedTecnico = new();
    [ObservableProperty]
    private string _searchTerm = string.Empty;
    private IFamiliaService FamiliaService { get; }
    private ILoggedUserService LoggedUserService { get; }
    private ICrasService CrasService { get; }
    private NavigationManager NavigationManager { get; }
    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);

    public FamiliaViewModel(IFamiliaService familiaService, ICrasService crasService, ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        FamiliaService = familiaService;
        CrasService = crasService;
        LoggedUserService = loggedUserService;
        NavigationManager = navigationManager;
    }
    
    [RelayCommand]
    private async Task GoToRegistrarFamilia()
    {
        NavigationManager.NavigateTo("/familia/registrar");
    }
    
    public async void GetAllProntuarios()
    {
        Prontuarios = new(await CrasService.GetProntuarioAndFamiliaAndUsuariosFromCras(LoggedTecnico.CrasInfo.Id));
    }
    [RelayCommand]
    private async Task SearchFamiliaByMembro()
    {
        
    }
    [RelayCommand]
    private async Task GoToEditarFamilia(int familiaId)
    {
        NavigationManager.NavigateTo($"/familias/editar/{familiaId}");
    }
}