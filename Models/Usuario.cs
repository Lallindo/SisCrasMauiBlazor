using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Usuario : ObservableObject
{
    public int Id;
    public string Nome;
    public ICollection<FamiliaUsuario> FamiliaUsuarios;
    public string Nis;
    public string? NomeSocial;
    public string Rg;
    public string Cpf;
    public DateOnly DataNascimento;
    public SexoEnum Sexo;
    public EstadoCivilEnum EstadoCivil;
    public OrientacaoSexualEnum OrientacaoSexual;
    public RacaEnum Raca;
    public EscolaridadeEnum Escolaridade;
    public float RendaBruta;
    public string Profissao;
    public string Ocupacao;
    public FonteRendaEnum FonteRenda;
}