using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Familia : ObservableObject
{
    [ObservableProperty]
    int _Id;
    [ObservableProperty]
    ICollection<Prontuario> _Prontuarios = [];
    [ObservableProperty]
    ICollection<FamiliaUsuario> _FamiliaUsuarios = [];
    [ObservableProperty]
    ConfiguracaoFamiliarEnum _ConfiguracaoFamiliar;
}