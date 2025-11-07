using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class BuscarFamiliaViewModel(IProntuarioService prontuarioService, IUsuarioService usuarioService) : BaseViewModel
{ 
    private IProntuarioService _prontuarioService = prontuarioService;
    private IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty] private Usuario _selectedUsuario = new();
    [ObservableProperty] private ICollection<Familia> _familias = [];

    [RelayCommand]
    private async Task GetProntuariosFromUsuario()
    {
       Usuario tempUsuario = await _usuarioService.GetByCpf(SelectedUsuario);
       Familias = await _usuarioService.GetFamiliasFromUsuario(tempUsuario);
    }
}