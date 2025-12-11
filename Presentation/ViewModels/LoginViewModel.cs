using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class LoginViewModel(ITecnicoService tecnicoService, NavigationManager navigationManager) : BaseViewModel
{
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ITecnicoService _tecnicoService = tecnicoService;
    [ObservableProperty] private bool _loginError;
    [ObservableProperty] private string? _loginErrorTxt = string.Empty;

    [ObservableProperty] private Tecnico _tecnico = new();

    [RelayCommand]
    private async Task TryLoginAsync()
    {
        var tryLogin = await _tecnicoService.TryLoginAsync(Tecnico.Login, Tecnico.Senha);

        if (tryLogin.CrasAtivo != null)
        {
            LoginError = false;
            _navigationManager.NavigateTo("/familias");
        }
        else
        {
            LoginError = true;
            LoginErrorTxt = "Usuário ou senha incorretos";
        }
        
        if (tryLogin.CrasAtivo == null)
        {
            LoginError = true;
            LoginErrorTxt = "Usuário não existe ou não está ligado a um CRAS";
            return;
        }
    }
}
