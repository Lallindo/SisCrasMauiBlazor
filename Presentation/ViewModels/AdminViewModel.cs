using System.Collections.ObjectModel;
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

    [ObservableProperty] private Tecnico? _tecnicoLogado = new();
    [ObservableProperty] private Tecnico _novoTecnico = new();
    public ObservableCollection<Tecnico> Tecnicos { get; set; } = [];

    public AdminViewModel(ILoggedUserService loggedUserService, ICrasService crasService, ITecnicoService tecnicoService)
    {
        _crasService = crasService;
        _loggedUserService = loggedUserService;

        TecnicoLogado = _loggedUserService.GetCurrentUser();
        Task.Run(BuscarTecnicos);
    }

    public async Task BuscarTecnicos()
    {
        Tecnicos = new(await _crasService.GetTecnicosFromCras(TecnicoLogado.CrasAtivo));
    }
    
    [RelayCommand]
    private Task AdicionarTecnico()
    {
        return _tecnicoService.AddAsync(NovoTecnico);
    }
    
    
}