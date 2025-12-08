using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

namespace SisCras.Presentation.ViewModels;

public partial class EditarFamiliaViewModel(IFamiliaService familiaService, IProntuarioService prontuarioService) : BaseViewModel
{
    private IProntuarioService _ProntuarioService = prontuarioService;
    private IFamiliaService _FamiliaService = familiaService;

    [ObservableProperty] private Prontuario? _selectedProntuario = new();

    [ObservableProperty] private ObservableCollection<Usuario> _usuarios = [];

    public async Task GetProntuario(int familiaId)
    {
        SelectedProntuario = await _ProntuarioService.GetProntuarioByFamiliaIdNoTracking(familiaId);
    }

    [RelayCommand]
    public async Task UpdateProntuario()
    {
        await _ProntuarioService.UpdateAsync(SelectedProntuario);
    }

    [RelayCommand]
    private async Task DeactivateUsuario(FamiliaUsuario usuario)
    {
        await SelectedProntuario.Familia.ToggleAtivoUsuario(usuario);
    }
    
    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        var usuarioCount = SelectedProntuario.Familia.FamiliaUsuarios.Count;
        SelectedProntuario.Familia.FamiliaUsuarios.Add(new FamiliaUsuario
        {
            Usuario = new Usuario(),
            Parentesco = usuarioCount == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Conjuge,
            DataAdicao = DateOnly.FromDateTime(DateTime.Now.AddDays(-3))
        });
    }
}
