using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components; // Adicione este using
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;
public partial class PaginaLoginViewModel(ITecnicoService tecnicoService, NavigationManager navigationManager) : BaseViewModel
{
    ITecnicoService _TecnicoService = tecnicoService;
    NavigationManager _NavigationManager = navigationManager; // Injete NavigationManager

    [ObservableProperty]
    Tecnico _Tecnico = new();
    [ObservableProperty]
    bool _LoginError = false;

    [RelayCommand]
    private async Task TryLoginAsync()
    {
        if (await _TecnicoService.TryLoginAsync(Tecnico.Login, Tecnico.Senha))
        {
            LoginError = false;
            _NavigationManager.NavigateTo("/home");
        }
        else
        {
            LoginError = true;
        }
    }
}