using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class PaginaTestesViewModel(ICrasService crasService) : ObservableObject
{
    ICrasService Service { get; set; } = crasService;

    [ObservableProperty]
    List<Cras>? _Cras = [];
    [ObservableProperty]
    List<Tecnico>? _Tecnicos = [];

    [RelayCommand]
    private async Task GetAllCras(string id)
    {
        _ = int.TryParse(id, out int result);
        Tecnicos = await Service.GetAllTecnicos(result);
    }
}