using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Familia : ObservableObject
{
    [ObservableProperty]
    private ConfiguracaoFamiliarEnum _configuracaoFamiliar;
    [ObservableProperty]
    private ICollection<FamiliaUsuario> _familiaUsuarios = [];
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private ICollection<Prontuario> _prontuarios = [];
}