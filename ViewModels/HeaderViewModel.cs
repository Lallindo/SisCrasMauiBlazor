using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Services;
using SisCras.Models;

namespace SisCras.ViewModels;

public partial class HeaderViewModel : ObservableObject
{
    ILoggedUserService _LoggedUserService { get; }

    public HeaderViewModel(ILoggedUserService loggedUserService)
    {
        _LoggedUserService = loggedUserService;
        _LoggedUserService.UserStateChanged += ChangeTecnicoState;
    }

    [ObservableProperty]
    Tecnico? _Tecnico = new(){Nome="Bruno", CrasInfo=new(0, "Testando")};
    [ObservableProperty]
    bool _IsTecnicoLoggedIn = true;

    private void ChangeTecnicoState()
    {
        IsTecnicoLoggedIn = !IsTecnicoLoggedIn;
        Tecnico = _LoggedUserService.GetCurrentUser();
    }

    [RelayCommand]
    private async Task LogoutTecnico()
    {
        _LoggedUserService.ClearCurrentUser();
        await Shell.Current.GoToAsync("//PaginaLogin");
    }
} 