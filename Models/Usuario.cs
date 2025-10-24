using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Usuario : ObservableObject
{
    [ObservableProperty]
    private string _cpf;
    [ObservableProperty]
    private DateOnly _dataNascimento;
    [ObservableProperty]
    private EscolaridadeEnum _escolaridade;
    [ObservableProperty]
    private EstadoCivilEnum _estadoCivil;
    [ObservableProperty]
    private ICollection<FamiliaUsuario> _familiaUsuarios;
    [ObservableProperty]
    private FonteRendaEnum _fonteRenda;
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _nis;
    [ObservableProperty]
    private string _nome;
    [ObservableProperty]
    private string? _nomeSocial;
    [ObservableProperty]
    private string _ocupacao;
    [ObservableProperty]
    private OrientacaoSexualEnum _orientacaoSexual;
    [ObservableProperty]
    private string _profissao;
    [ObservableProperty]
    private RacaEnum _raca;
    [ObservableProperty]
    private float _rendaBruta;
    [ObservableProperty]
    private string _rg;
    [ObservableProperty]
    private SexoEnum _sexo;
}