using System.Collections.ObjectModel;
using System.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class AdminViewModel : ObservableObject
{
    private ITecnicoService _tecnicoService { get; }
    private ICrasService _crasService { get; }
    private ILoggedUserService _loggedUserService { get; }
    private IPasswordService _passwordService { get; }

    [ObservableProperty] private Tecnico? _tecnicoLogado = new();
    [ObservableProperty] private Tecnico _novoTecnico = new();
    [ObservableProperty] private string _confirmarSenha = String.Empty;
    [ObservableProperty] private bool _buscandoTecnicos = false;
    public ObservableCollection<Tecnico> Tecnicos { get; set; } = [];

    public AdminViewModel(ILoggedUserService loggedUserService, ICrasService crasService, ITecnicoService tecnicoService, IPasswordService passwordService)
    {
        _tecnicoService = tecnicoService;
        _crasService = crasService;
        _loggedUserService = loggedUserService;
        _passwordService = passwordService;

        TecnicoLogado = _loggedUserService.GetCurrentUser();
        Task.Run(BuscarTecnicos);
    }

    public async Task BuscarTecnicos()
    {
        BuscandoTecnicos = false;
        Tecnicos = new(await _crasService.GetTecnicosFromCras(TecnicoLogado.CrasAtivo));
        BuscandoTecnicos = true;
    }
    
    [RelayCommand]
    private async Task<Tecnico> AdicionarTecnico()
    {
        NovoTecnico.TecnicoCras.Add(new()
        {
            Id = 0,
            CrasId = TecnicoLogado.CrasAtivo.Id,
            Cras = TecnicoLogado.CrasAtivo,
            Tecnico = NovoTecnico,
            TecnicoId = 0,
            DataEntrada = DateOnly.FromDateTime(DateTime.Now)
        });
        Tecnico tecnicoComHash = await _tecnicoService.ChangeSenhaForHash(NovoTecnico, _passwordService);
        return await _tecnicoService.AddAsync(tecnicoComHash);
    }
    
    
}