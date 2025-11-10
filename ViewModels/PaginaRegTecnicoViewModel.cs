using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Services;
using SisCras.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components; // Adicione este using
using System.Diagnostics;
using SisCras.Models.ValueObjects;

namespace SisCras.ViewModels;

public partial class PaginaRegTecnicoViewModel : BaseViewModel
{
    public PaginaRegTecnicoViewModel(ITecnicoService tecnicoService, ICrasService crasService, IPasswordService passwordService, NavigationManager navigationManager)
    {
        TecnicoService = tecnicoService;
        CrasService = crasService;
        PasswordService = passwordService;
        NavigationManager = navigationManager;

        TodosCras = [.. CrasService.GetAllAsync().Result]; 
    }

    ITecnicoService TecnicoService { get; }
    ICrasService CrasService { get; }
    IPasswordService PasswordService { get; }
    NavigationManager NavigationManager { get; }

    [ObservableProperty]
    List<Cras> _todosCras;
    [ObservableProperty]
    Tecnico _tecnico = new();
    [ObservableProperty]
    Cras? _crasSelecionado = null;
    [ObservableProperty]
    bool _erroRegistro = false;

    [RelayCommand]
    private async Task RegistrarTecnico()
    {
        if (Tecnico.Nome != null && Tecnico.Login != null && Tecnico.Senha != null && CrasSelecionado != null)
        {
            try
            {
                Tecnico.ChangeSenhaForHash(Tecnico.Senha, PasswordService);

                await TecnicoService.AddAsync(Tecnico);

                TecnicoCras tecnicoCras = new()
                {
                    TecnicoId = Tecnico.Id,
                    CrasId = CrasSelecionado.Id,
                    DataEntrada = DateOnly.FromDateTime(DateTime.Today)
                };

                Tecnico.TecnicoCras = [tecnicoCras];
                await TecnicoService.UpdateAsync(Tecnico);

                ErroRegistro = false;
                NavigationManager.NavigateTo("/login"); // Use NavigationManager
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