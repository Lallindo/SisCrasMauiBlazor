using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class PaginaListagemViewModel(ICrasService crasService, ILoggedUserService loggedUserService, NavigationManager navigationManager) : BaseViewModel
{
    private Familia? _familiaParaRemover;
    [ObservableProperty]
    private ObservableCollection<Familia> _familias = [];

    [ObservableProperty]
    private Tecnico? _loggedTecnico = new();
    [ObservableProperty]
    private string _searchTerm = string.Empty;
    private ICrasService CrasService { get; } = crasService;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    private NavigationManager NavigationManager { get; } = navigationManager;
    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);

    [RelayCommand]
    private async static Task GoToRegistramento()
    {
        await Shell.Current.GoToAsync("PaginaRegistramento");
    }
    [RelayCommand]
    private void ClearSearch()
    {
        SearchTerm = string.Empty;
    }
    [RelayCommand]
    private void GoToRegistrarFamilia()
    {
        NavigationManager.NavigateTo("/familia/nova");
    }
    [RelayCommand]
    private void ViewFamilia(Familia familia)
    {
        if (familia == null) return;

        NavigationManager.NavigateTo($"/familias/editar/{familia.Id}");
    }

    [RelayCommand]
    private async Task GetAllFamilias()
    {
        Familias = new ObservableCollection<Familia>(await CrasService.GetFamiliasFromCras(LoggedUserService.GetCurrentUser().CrasInfo.Id));
    }
    [RelayCommand]
    private void RequestDeleteFamilia(Familia familia)
    {
        if (familia == null) return;

        // TODO
    }
    [RelayCommand]
    private Task SelectFamily(Familia familia)
    {
        return Task.CompletedTask;
    }
}