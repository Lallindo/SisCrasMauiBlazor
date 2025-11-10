using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class TecnicoCras : ObservableObject
{
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo { get => DataSaida != null; }
    
    public Tecnico Tecnico { get; set; }
    public Cras Cras { get; set; }
    public DateOnly DataEntrada { get; set; }
    public DateOnly? DataSaida { get; set; } = null;
}