using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class TecnicoCras : ObservableObject
{
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo { get => DataSaida != null; }

    [ObservableProperty]
    Tecnico _Tecnico;
    [ObservableProperty]
    Cras _Cras;
    [ObservableProperty]
    DateOnly _DataEntrada;
    [ObservableProperty]
    DateOnly? _DataSaida = null;
}