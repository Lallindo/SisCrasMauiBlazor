using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Services;
using SisCras.Models;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using SisCras.Models.ValueObjects;

namespace SisCras.ViewModels;

public partial class PaginaRegTecnicoViewModel : ObservableObject
{
    public PaginaRegTecnicoViewModel(ITecnicoService tecnicoService, ICrasService crasService, IPasswordService passwordService)
    {
        _TecnicoService = tecnicoService;
        _CrasService = crasService;
        _PasswordService = passwordService;

        TodosCras = [.. _CrasService.GetAllAsync().Result]; 
    }

    ITecnicoService _TecnicoService { get; }
    ICrasService _CrasService { get; }
    IPasswordService _PasswordService { get; }

    [ObservableProperty]
    List<Cras> _TodosCras;
    [ObservableProperty]
    Tecnico _Tecnico = new();
    [ObservableProperty]
    Cras? _CrasSelecionado = null;
    [ObservableProperty]
    bool _ErroRegistro = false;

    [RelayCommand]
    private async Task RegistrarTecnico()
    {
        if (Tecnico.Nome != null && Tecnico.Login != null && Tecnico.Senha != null && CrasSelecionado != null)
        {
            try
            {
                Tecnico.ChangeSenhaForHash(Tecnico.Senha, _PasswordService);

                await _TecnicoService.AddAsync(Tecnico);

                TecnicoCras tecnicoCras = new()
                {
                    TecnicoId = Tecnico.Id,
                    CrasId = CrasSelecionado.Id,
                    DataEntrada = DateOnly.FromDateTime(DateTime.Today)
                };

                Tecnico.TecnicoCras = [tecnicoCras];
                await _TecnicoService.UpdateAsync(Tecnico);

                ErroRegistro = false;
                await Shell.Current.GoToAsync("//PaginaLogin");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao registrar técnico: {ex.Message}");
                ErroRegistro = true;
            }
        } else
        {
            ErroRegistro = true;
        }
    } 
} 