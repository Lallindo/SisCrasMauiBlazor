using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class EditarFamiliaViewModel(IFamiliaService familiaService, IProntuarioService prontuarioService) : BaseViewModel
{
    private IProntuarioService _prontuarioService = prontuarioService;
    private IFamiliaService _FamiliaService = familiaService;

    // TODO Alterar para buscar o prontuário da família ao invés família
    [ObservableProperty] private Familia _selectedFamilia = new();

    [ObservableProperty] private ObservableCollection<Usuario> _usuarios = [];

    public async Task GetProntuario(int familiaId)
    {
        SelectedFamilia = await _FamiliaService.GetByIdAsync(familiaId) ?? new Familia();
    }

    [RelayCommand]
    public async Task UpdateFamilia(Familia familia)
    {
        await _FamiliaService.UpdateAsync(familia);
    }

    [RelayCommand]
    private async Task DeactivateUsuario(FamiliaUsuario usuario)
    {
        await SelectedFamilia.ToggleAtivoUsuario(usuario);
    }
    
}