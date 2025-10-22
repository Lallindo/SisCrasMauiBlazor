using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Cras : ObservableObject
{
    [ObservableProperty]
    int _Id;
    [ObservableProperty]
    string _Nome;
    [ObservableProperty]
    ICollection<TecnicoCras> _TecnicosCras;
    [ObservableProperty]
    ICollection<Prontuario> _Prontuarios;
}