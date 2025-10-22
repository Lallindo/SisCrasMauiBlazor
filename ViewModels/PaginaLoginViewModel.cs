using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class PaginaLoginViewModel(ITecnicoService tecnicoService, IPasswordService passwordService) : ObservableObject
{
    ITecnicoService _TecnicoService = tecnicoService;
    IPasswordService _PasswordService = passwordService;

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
            await Shell.Current.GoToAsync("PaginaListagem");
        }
        else
        {
            LoginError = true;
        }
    }
}