using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using System;
using SisCras.Services;
using SisCras.Models;

namespace SisCras.ViewModels;

public partial class HeaderViewModel : BaseViewModel, IDisposable
{
    private readonly ILoggedUserService _LoggedUserService;
    private readonly NavigationManager _NavigationManager;

    public HeaderViewModel(ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        _LoggedUserService = loggedUserService;
        _NavigationManager = navigationManager; 
        _LoggedUserService.UserStateChanged += ChangeTecnicoState;
        // Sincroniza o estado inicial ao ser criado
        ChangeTecnicoState();
    }

    [ObservableProperty]
    Tecnico? _Tecnico = new();
    [ObservableProperty]
    bool _IsTecnicoLoggedIn;

    private void ChangeTecnicoState()
    {
        IsTecnicoLoggedIn = _LoggedUserService.GetCurrentUser() != null;
        Tecnico = _LoggedUserService.GetCurrentUser();
    }

    public void GoToLogin()
    {
        _NavigationManager.NavigateTo("/");
    }

    [RelayCommand]
    private async Task LogoutTecnico()
    {
        _LoggedUserService.ClearCurrentUser();
        _NavigationManager.NavigateTo("/");
    }

    public void Dispose()
    {
        _LoggedUserService.UserStateChanged -= ChangeTecnicoState;
    }
} 