using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

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

    [ObservableProperty] private Prontuario _prontuario = new()
    {
        DataCriacao = DateOnly.FromDateTime(DateTime.Now),
        Familia = new()
        {
            FamiliaUsuarios = []
        }
    };
    [ObservableProperty] private Usuario? _usuario = new();

    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        var usuarioCount = Prontuario.Familia.FamiliaUsuarios.Count;
        Prontuario.Familia.FamiliaUsuarios.Add(new FamiliaUsuario
        {
            Usuario = new Usuario
            {
                Nome = usuarioCount == 1 ? Usuario.Nome : "",
                Cpf = usuarioCount == 1 ? Usuario.Nome : "",
                Nis = usuarioCount == 1 ? Usuario.Nome : ""
            },
            Parentesco = usuarioCount == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Conjuge,
            DataAdicao = DateOnly.FromDateTime(DateTime.Now.AddDays(-3))
        });
    }

    [RelayCommand]
    private async Task InsertNewProntuario()
    {
        Prontuario.Tecnico = _loggedUserService.GetCurrentUser();
        Prontuario.Cras = Prontuario.Tecnico.CrasAtivo;
        Prontuario.Id = 0;
        await _prontuarioService.AddAsync(Prontuario);
    }
}