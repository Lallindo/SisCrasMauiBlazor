using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Domain.Entities;

public partial class TecnicoCras : ObservableObject
{
    [ObservableProperty] private Cras _cras;
    [ObservableProperty] private Tecnico _tecnico;
    [ObservableProperty] private DateOnly _dataEntrada;
    [ObservableProperty] private DateOnly? _dataSaida;
    public int Id { get; set; }
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo => DataSaida != null;
}