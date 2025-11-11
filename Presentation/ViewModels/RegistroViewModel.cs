using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Domain.Entities.Enums;

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

    [ObservableProperty] private Prontuario _prontuario = new();
    [ObservableProperty] private ObservableCollection<FamiliaUsuario> _familiaUsuarios = [];
    [ObservableProperty] private Usuario? _usuario = new();

    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        var usuarioCount = _familiaUsuarios.Count;
        FamiliaUsuarios.Add(new FamiliaUsuario
        {
            Usuario = new Usuario
            {
                Nome = usuarioCount == 1 ? Usuario.Nome : "",
                Cpf = usuarioCount == 1 ? Usuario.Nome : "",
                Nis = usuarioCount == 1 ? Usuario.Nome : ""
            },
            Parentesco = usuarioCount == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Conjuge
        });
    }
}