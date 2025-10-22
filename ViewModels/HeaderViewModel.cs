using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components; // Adicione este using
using SisCras.Services;
using SisCras.Models;

namespace SisCras.ViewModels;

public partial class HeaderViewModel : ObservableObject
{
    private readonly ILoggedUserService _LoggedUserService;
    private readonly NavigationManager _NavigationManager; // Injete NavigationManager

    public HeaderViewModel(ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        _LoggedUserService = loggedUserService;
        _NavigationManager = navigationManager; // Injete NavigationManager
        _LoggedUserService.UserStateChanged += ChangeTecnicoState;
    }

    [ObservableProperty]
    Tecnico? _Tecnico = new();
    [ObservableProperty]
    bool _IsTecnicoLoggedIn = false;

    private void ChangeTecnicoState()
    {
        IsTecnicoLoggedIn = !IsTecnicoLoggedIn;
        Tecnico = _LoggedUserService.GetCurrentUser();
    }

    public void GoToLogin()
    {
        _NavigationManager.NavigateTo("/login");
    }

    [RelayCommand]
    private async Task LogoutTecnico()
    {
        _LoggedUserService.ClearCurrentUser();
        _NavigationManager.NavigateTo("/login"); // Use NavigationManager
    }
} 