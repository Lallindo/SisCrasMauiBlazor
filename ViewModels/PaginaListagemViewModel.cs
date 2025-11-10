using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;
public partial class PaginaListagemViewModel(ITecnicoService tecnicoService) : BaseViewModel
{
    ITecnicoService TecnicoService { get; } = tecnicoService;

    [ObservableProperty]
    private ObservableCollection<Tecnico> _tecnicos;

    [RelayCommand]
    private async void GetAllTecnicosAsync()
    {
        Tecnicos = [.. await TecnicoService.GetAllAsync()];
    }

    [RelayCommand]
    private async void GoToRegistramento()
    {
        await Shell.Current.GoToAsync("PaginaRegistramento");
    }
}