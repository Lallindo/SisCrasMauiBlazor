using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class HeaderViewModel : BaseViewModel, IDisposable
{
    private readonly ILoggedUserService _loggedUserService;
    private readonly NavigationManager _navigationManager;
    
    [ObservableProperty]
    private bool _isTecnicoLoggedIn;
    [ObservableProperty]
    private Tecnico? _tecnico = new();

    public HeaderViewModel(ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        _loggedUserService = loggedUserService;
        _navigationManager = navigationManager;
        _loggedUserService.UserStateChanged += ChangeTecnicoState;
        ChangeTecnicoState();
    }

    public void Dispose()
    {
        _loggedUserService.UserStateChanged -= ChangeTecnicoState;
    }

    private void ChangeTecnicoState()
    {
        IsTecnicoLoggedIn = _loggedUserService.GetCurrentUser() != null;
        Tecnico = _loggedUserService.GetCurrentUser();
    }

    public void GoToLogin()
    {
        _navigationManager.NavigateTo("/");
    }

    [RelayCommand]
    private async Task LogoutTecnico()
    {
        _loggedUserService.ClearCurrentUser();
        _navigationManager.NavigateTo("/");
    }
}