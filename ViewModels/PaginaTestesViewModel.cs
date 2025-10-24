using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class PaginaTestesViewModel(
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