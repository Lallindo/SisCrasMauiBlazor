using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class TecnicoCras : ObservableObject
{
    public int Id { get; private set; }
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo { get => DataSaida != null; }

    [ObservableProperty]
    private Tecnico _tecnico;
    [ObservableProperty]
    private Cras _cras;
    [ObservableProperty]
    private DateOnly _dataEntrada;
    [ObservableProperty]
    private DateOnly? _dataSaida = null;
}