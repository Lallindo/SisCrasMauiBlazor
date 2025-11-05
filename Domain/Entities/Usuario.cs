using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Entities.Enums;

namespace SisCras.Domain.Entities;

public partial class Usuario : ObservableObject
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _nome;
    [ObservableProperty] private string? _nomeSocial;
    [ObservableProperty] private string _cpf;
    [ObservableProperty] private string _rg;
    [ObservableProperty] private ICollection<FamiliaUsuario> _familiaUsuarios;
    [ObservableProperty] private DateOnly _dataNascimento;
    [ObservableProperty] private string _nis;
    [ObservableProperty] private string _ocupacao;
    [ObservableProperty] private float _rendaBruta;
    [ObservableProperty] private string _profissao;
    [ObservableProperty] private OrientacaoSexualEnum _orientacaoSexual;
    [ObservableProperty] private RacaEnum _raca;
    [ObservableProperty] private EstadoCivilEnum _estadoCivil;
    [ObservableProperty] private EscolaridadeEnum _escolaridade;
    [ObservableProperty] private SexoEnum _sexo;
    [ObservableProperty] private FonteRendaEnum _fonteRenda;
}