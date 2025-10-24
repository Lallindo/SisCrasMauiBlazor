using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using System;
using SisCras.Services;
using SisCras.Models;

namespace SisCras.ViewModels;

public partial class HeaderViewModel : BaseViewModel, IDisposable
{
    private readonly ILoggedUserService _loggedUserService;
    private readonly NavigationManager _navigationManager;

    public HeaderViewModel(ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        _loggedUserService = loggedUserService;
        _navigationManager = navigationManager; 
        _loggedUserService.UserStateChanged += ChangeTecnicoState;
        // Sincroniza o estado inicial ao ser criado
        ChangeTecnicoState();
    }

    [ObservableProperty]
    private Tecnico? _tecnico = new();
    [ObservableProperty]
    private bool _isTecnicoLoggedIn;

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

    public void Dispose()
    {
        _loggedUserService.UserStateChanged -= ChangeTecnicoState;
    }
} 