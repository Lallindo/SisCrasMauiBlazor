using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;
public partial class PaginaListagemViewModel(IFamiliaService familiaService, ILoggedUserService loggedUserService, NavigationManager navigationManager) : BaseViewModel
{
    private IFamiliaService FamiliaService { get; } = familiaService;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    private NavigationManager NavigationManager { get; } = navigationManager;
    private int _crasTecnicoId;
    private bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);
    private Familia? _familiaParaRemover;

    [ObservableProperty]
    private Tecnico? _loggedTecnico = new();
    [ObservableProperty]
    private ObservableCollection<Familia> _familias = new();
    [ObservableProperty]
    private string _searchTerm = string.Empty;

    [RelayCommand]
    private async Task GoToRegistramento()
    {
        await Shell.Current.GoToAsync("PaginaRegistramento");
    }
    [RelayCommand]
    private async Task OnAppearingAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            LoggedTecnico = LoggedUserService.GetCurrentUser();
            _crasTecnicoId = LoggedTecnico?.CrasInfo.Id ?? 0;
            Familias = new(await FamiliaService.GetAllAsync());
        }
        catch (Exception ex)
        {
            // TODO
        }
        finally
        {
            IsBusy = false;
        }
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
    private void RequestDeleteFamilia(Familia familia)
    {
        if (familia == null) return;

        // TODO CONTINUAR
    }
}