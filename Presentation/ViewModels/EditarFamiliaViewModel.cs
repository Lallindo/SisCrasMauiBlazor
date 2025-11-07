using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.ViewModels;

public partial class EditarFamiliaViewModel(IFamiliaService familiaService) : BaseViewModel
{
    IFamiliaService _FamiliaService = familiaService;

    [ObservableProperty] 
    private Familia _selectedFamilia = new();

    [ObservableProperty] 
    private ObservableCollection<Usuario> _usuarios = [];
    
    public async Task GetFamilia(int familiaId)
    {
        SelectedFamilia = await _FamiliaService.GetByIdAsync(familiaId)?? new Familia();
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