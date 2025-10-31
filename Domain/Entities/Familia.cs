using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Entities.Enums;

namespace SisCras.Domain.Entities;

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