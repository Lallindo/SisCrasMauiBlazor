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
    IFamiliaService _FamiliaService { get; } = familiaService;
    ILoggedUserService _LoggedUserService { get; } = loggedUserService;
    NavigationManager _NavigationManager { get; } = navigationManager;
    int _CrasTecnicoId;
    bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);
    Familia? _FamiliaParaRemover;

    [ObservableProperty]
    Tecnico? _LoggedTecnico = new();
    [ObservableProperty]
    ObservableCollection<Familia> _Familias = new();
    [ObservableProperty]
    string _SearchTerm = string.Empty;

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
            LoggedTecnico = _LoggedUserService.GetCurrentUser();
            _CrasTecnicoId = LoggedTecnico?.CrasInfo.Id ?? 0;
            Familias = new(await _FamiliaService.GetAllAsync());
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
        _NavigationManager.NavigateTo("/familia/nova");
    }
    [RelayCommand]
    private void ViewFamilia(Familia familia)
    {
        if (familia == null) return;

        _NavigationManager.NavigateTo($"/familias/editar/{familia.Id}");
    }
    [RelayCommand]
    private void RequestDeleteFamilia(Familia familia)
    {
        if (familia == null) return;

        // TODO CONTINUAR
    }
}