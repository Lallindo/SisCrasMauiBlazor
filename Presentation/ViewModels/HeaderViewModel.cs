using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class HeaderViewModel : BaseViewModel, IDisposable
{
    private readonly ILoggedUserService _loggedUserService;
    private readonly NavigationManager _navigationManager;

    [ObservableProperty] private bool _isTecnicoLoggedIn;
    [ObservableProperty] private Tecnico? _tecnico = new();

    public HeaderViewModel(ILoggedUserService loggedUserService, NavigationManager navigationManager)
    {
        _loggedUserService = loggedUserService;
        _navigationManager = navigationManager;
        _loggedUserService.UserStateChanged += ChangeTecnicoState;
        ChangeTecnicoState();
        Debug.WriteLine($"IsAdmin: {Tecnico.IsAdmin}, Cras: {Tecnico.CrasAtivo.Nome}");
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

    [RelayCommand]
    private Task LogoutTecnico()
    {
        _loggedUserService.ClearCurrentUser();
        return GoToLogin();
    }

    [RelayCommand]
    private Task GoToLogin()
    {
        _navigationManager.NavigateTo("/");
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task GoToHome()
    {
        _navigationManager.NavigateTo("/familias");
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task GoToAdmin()
    {
        _navigationManager.NavigateTo("/admin");
        return Task.CompletedTask;
    }
}