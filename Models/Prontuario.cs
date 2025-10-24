using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Prontuario : ObservableObject
{
    public int TecnicoId { get; set; }
    public int FamiliaId { get; set; }
    public int CrasId { get; set; }

    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private int _codigo;
    [ObservableProperty]
    private Tecnico _tecnico;
    [ObservableProperty]
    private Familia _familia;
    [ObservableProperty]
    private Cras _cras;
    [ObservableProperty]
    private string _formaDeAcesso;
    [ObservableProperty]
    private DateOnly _dataCriacao;
    [ObservableProperty]
    private DateOnly? _dataSaida = null;

    public bool Ativo => _dataSaida != null; 
}
