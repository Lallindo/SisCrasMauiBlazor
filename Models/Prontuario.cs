using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Prontuario : ObservableObject
{
    [ObservableProperty]
    private int _codigo;
    [ObservableProperty]
    private Cras _cras;
    [ObservableProperty]
    private DateOnly _dataCriacao;
    [ObservableProperty]
    private DateOnly? _dataSaida;
    [ObservableProperty]
    private Familia _familia;
    [ObservableProperty]
    private string _formaDeAcesso;

    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private Tecnico _tecnico;
    public int TecnicoId { get; set; }
    public int FamiliaId { get; set; }
    public int CrasId { get; set; }

    public bool Ativo => _dataSaida != null;
}