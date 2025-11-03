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
    
    public async Task GetFamilia(int familiaId)
    {
        SelectedFamilia = await _FamiliaService.GetByIdAsync(familiaId)?? new Familia();
    }
}