using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Prontuario : ObservableObject
{
    public int TecnicoId { get; set; }
    public int FamiliaId { get; set; }
    public int CrasId { get; set; }

    [ObservableProperty]
    int _Id;
    [ObservableProperty]
    int _Codigo;
    [ObservableProperty]
    Tecnico _Tecnico;
    [ObservableProperty]
    Familia _Familia;
    [ObservableProperty]
    Cras _Cras;
    [ObservableProperty]
    string _FormaDeAcesso;
    [ObservableProperty]
    DateOnly _DataCriacao;
    [ObservableProperty]
    DateOnly? _DataSaida = null;

    public bool Ativo => _DataSaida != null; 
}
