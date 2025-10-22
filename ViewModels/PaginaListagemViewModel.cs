using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;
public partial class PaginaListagemViewModel(ITecnicoService tecnicoService) : BaseViewModel
{
    ITecnicoService _TecnicoService { get; } = tecnicoService;

    [ObservableProperty]
    private ObservableCollection<Tecnico> _Tecnicos;

    [RelayCommand]
    private async void GetAllTecnicosAsync()
    {
        Tecnicos = [.. await _TecnicoService.GetAllAsync()];
    }

    [RelayCommand]
    private async void GoToRegistramento()
    {
        await Shell.Current.GoToAsync("PaginaRegistramento");
    }
}