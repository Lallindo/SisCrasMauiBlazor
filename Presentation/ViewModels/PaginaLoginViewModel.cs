using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

// Adicione este using

namespace SisCras.Presentation.ViewModels;

public partial class PaginaLoginViewModel(ITecnicoService tecnicoService, NavigationManager navigationManager) : BaseViewModel
{
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ITecnicoService _tecnicoService = tecnicoService;
    [ObservableProperty]
    private bool _loginError;

    [ObservableProperty]
    private Tecnico _tecnico = new();

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