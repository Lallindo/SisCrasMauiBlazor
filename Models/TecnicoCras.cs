using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class TecnicoCras : ObservableObject
{
    [ObservableProperty]
    private Cras _cras;
    [ObservableProperty]
    private DateOnly _dataEntrada;
    [ObservableProperty]
    private DateOnly? _dataSaida;

    [ObservableProperty]
    private Tecnico _tecnico;
    public int Id { get; private set; }
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo => DataSaida != null;
}