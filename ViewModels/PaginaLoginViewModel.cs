using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components; // Adicione este using
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;
public partial class PaginaLoginViewModel(ITecnicoService tecnicoService, NavigationManager navigationManager) : BaseViewModel
{
    private ITecnicoService _tecnicoService = tecnicoService;
    private NavigationManager _navigationManager = navigationManager; 

    [ObservableProperty]
    private Tecnico _tecnico = new();
    [ObservableProperty]
    private bool _loginError = false;

    [RelayCommand]
    private async Task TryLoginAsync()
    {
        if (await _tecnicoService.TryLoginAsync(Tecnico.Login, Tecnico.Senha))
        {
            LoginError = false;
            _navigationManager.NavigateTo("/home");
        }
        else
        {
            LoginError = true;
        }
    }
}