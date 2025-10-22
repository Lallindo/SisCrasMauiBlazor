using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Usuario : ObservableObject
{
    [ObservableProperty]
    int _Id;
    [ObservableProperty]
    string _Nome;
    [ObservableProperty]
    ICollection<FamiliaUsuario> _FamiliaUsuarios;
    [ObservableProperty]
    string _Nis;
    [ObservableProperty]
    string? _NomeSocial;
    [ObservableProperty]
    string _Rg;
    [ObservableProperty]
    string _Cpf;
    [ObservableProperty]
    DateOnly _DataNascimento;
    [ObservableProperty]
    SexoEnum _Sexo;
    [ObservableProperty]
    EstadoCivilEnum _EstadoCivil;
    [ObservableProperty]
    OrientacaoSexualEnum _OrientacaoSexual;
    [ObservableProperty]
    RacaEnum _Raca;
    [ObservableProperty]
    EscolaridadeEnum _Escolaridade;
    [ObservableProperty]
    float _RendaBruta;
    [ObservableProperty]
    string _Profissao;
    [ObservableProperty]
    string _Ocupacao;
    [ObservableProperty]
    FonteRendaEnum _FonteRenda;
}