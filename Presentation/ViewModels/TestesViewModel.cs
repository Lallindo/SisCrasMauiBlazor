using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class TestesViewModel(
    IUsuarioService usuarioService,
    ICrasService crasService,
    ITecnicoService tecnicoService,
    IFamiliaService familiaService,
    IProntuarioService prontuarioService
    ) : BaseViewModel
{
    private readonly ICrasService _crasService = crasService;
    private readonly IFamiliaService _familiaService = familiaService;
    private readonly IProntuarioService _prontuarioService = prontuarioService;
    private readonly ITecnicoService _tecnicoService = tecnicoService;
    private readonly IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty]
    private Usuario? _usuario;

    [RelayCommand]
    private async Task BuscarFamilias()
    {
        Usuario = await _familiaService.GetResponsavelFromFamilia(1);
    }
}