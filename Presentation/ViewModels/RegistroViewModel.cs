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
    ILoggedUserService loggedUserService,
    IUsuarioService usuarioService
) : BaseViewModel
{
    private readonly IFamiliaService _familiaService = familiaService;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;
    private readonly IProntuarioService _prontuarioService = prontuarioService;
    private readonly IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty] private Prontuario _prontuario = new()
    {
        DataCriacao = DateOnly.FromDateTime(DateTime.Now),
        Familia = new()
        {
            FamiliaUsuarios = []
        }
    };
    [ObservableProperty] private Usuario? _usuario = new();
    [ObservableProperty] private ObservableCollection<Prontuario> _prontuariosEncontrados = [];

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
    private async Task VerifyDuplicate(Usuario usuarioToVerify)
    {
        if (usuarioToVerify is null) return;
        
        bool temDados = !string.IsNullOrWhiteSpace(usuarioToVerify.Nome) || 
                        !string.IsNullOrWhiteSpace(usuarioToVerify.Cpf) || 
                        !string.IsNullOrWhiteSpace(usuarioToVerify.Nis);

        if (!temDados) return;

        try
        {
            var resultado = await _usuarioService.GetUsuarioByUsuarioSearch(
                usuarioToVerify.Nome,
                usuarioToVerify.Cpf,
                usuarioToVerify.Nis
            );
            
            if (resultado != null)
            {
                foreach (var f in resultado.FamiliaUsuarios)
                {
                    Debug.WriteLine($"{f.Parentesco}");
                }
            }
            else
            {
                Debug.WriteLine("Nada encontrado");
            }
        } catch (Exception ex)
        {
            Debug.WriteLine($"Erro na busca: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task InsertNewProntuario()
    {
        Prontuario.Tecnico = _loggedUserService.GetCurrentUser();
        Prontuario.Cras = Prontuario.Tecnico?.CrasAtivo;
        Prontuario.Id = 0;
        await _prontuarioService.AddAsync(Prontuario);
    }
}
