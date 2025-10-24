using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Cras : ObservableObject
{
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _nome;
    [ObservableProperty]
    private ICollection<Prontuario> _prontuarios;
    [ObservableProperty]
    private ICollection<TecnicoCras> _tecnicosCras;
}