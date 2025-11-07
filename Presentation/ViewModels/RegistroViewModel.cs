using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class RegistroViewModel(
    IProntuarioService prontuarioService,
    IFamiliaService familiaService,
    ILoggedUserService loggedUserService
    ) : BaseViewModel
{
    private readonly IFamiliaService _familiaService = familiaService;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;
    private readonly IProntuarioService _prontuarioService = prontuarioService;

    [ObservableProperty]
    private Prontuario _prontuario = new();
    [ObservableProperty]
    private ObservableCollection<FamiliaUsuario> _familiaUsuarios = [new()];

    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        FamiliaUsuarios.Add(new FamiliaUsuario() {Usuario = new Usuario()});
    }
}