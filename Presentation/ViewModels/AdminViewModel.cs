using System.Collections.ObjectModel;
using System.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Havit.Blazor.Components.Web.Bootstrap;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Presentation.DTOs;

namespace SisCras.Presentation.ViewModels;

public partial class AdminViewModel : ObservableObject
{
    private ITecnicoService _tecnicoService { get; }
    private ICrasService _crasService { get; }
    private ILoggedUserService _loggedUserService { get; }
    private IPasswordService _passwordService { get; }

    [ObservableProperty] private Tecnico? _tecnicoLogado = new();
    [ObservableProperty] private TecnicoDto _novoTecnico = new();
    [ObservableProperty] private bool _buscandoTecnicos = false;
    [ObservableProperty] private ObservableCollection<Tecnico> _tecnicos = [];

    public HxModal ConfirmationModal;

    [ObservableProperty] private Tecnico? _tecnicoParaRemover;
    [ObservableProperty] private string _senhaUsuarioExclusao = string.Empty;
    [ObservableProperty] private string _erroModal = string.Empty;
    
    public AdminViewModel(ILoggedUserService loggedUserService, ICrasService crasService, ITecnicoService tecnicoService, IPasswordService passwordService)
    {
        _tecnicoService = tecnicoService;
        _crasService = crasService;
        _loggedUserService = loggedUserService;
        _passwordService = passwordService;
        TecnicoLogado = _loggedUserService.GetCurrentUser();
    }

    public async Task BuscarTecnicos()
    {
        BuscandoTecnicos = true;
        Tecnicos = new(await _tecnicoService.GetAllTecnicos());
        BuscandoTecnicos = false;
    }
    
    [RelayCommand]
    private async Task<Tecnico?> AdicionarTecnico()
    {
        Tecnico novoTecnico = new()
        {
            Nome = NovoTecnico.Nome,
            Login = NovoTecnico.Login,
            Senha = _passwordService.CreatePassword(NovoTecnico.Senha)
        };
        novoTecnico.TecnicoCras.Add(new()
        {
            Id = 0,
            CrasId = TecnicoLogado.CrasAtivo.Id,
            Cras = TecnicoLogado.CrasAtivo,
            Tecnico = novoTecnico,
            TecnicoId = 0,
            DataEntrada = DateOnly.FromDateTime(DateTime.Now)
        });
        
        await _tecnicoService.AddAsync(novoTecnico);
        await BuscarTecnicos();

        return novoTecnico;
    }

    [RelayCommand]
    private async Task SolicitarRemocao(Tecnico tecnico)
    {
        TecnicoParaRemover = tecnico;
        if (TecnicoParaRemover.IsAdmin) return;
        SenhaUsuarioExclusao = string.Empty;
        ErroModal = string.Empty;

        await ConfirmationModal.ShowAsync();
    }

    [RelayCommand]
    private async Task RemoverTecnico()
    {
        if (TecnicoParaRemover == null || TecnicoLogado == null) return;
        bool senhaValida = _passwordService.VerifyPassword(SenhaUsuarioExclusao, TecnicoParaRemover.Senha);

        if (!senhaValida)
        {
            ErroModal = "Senha incorreta. Verifique a senha do técnico que deseja remover.";
            return;
        }

        try
        {
            await _tecnicoService.DeleteAsync(TecnicoParaRemover);
            await ConfirmationModal.HideAsync();
        }
        catch (Exception ex)
        {
            ErroModal = $"Erro ao remover: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task ReactivateTecnico(Tecnico tecnico)
    {
        await _tecnicoService.ReactivateTecnico(tecnico);
    }

    [RelayCommand]
    private async Task ImportTecnico(Tecnico tecnico)
    {
        await _tecnicoService.ImportTecnico(tecnico);
    }
}
