using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Enums;

namespace SisCras.Domain.Entities;

public class Usuario
{
    public int Id { get; set; } = 0;
    public string Nome { get; set; } = string.Empty;
    public string? NomeSocial { get; set; } = null;
    public string Cpf { get; set; } = string.Empty;
    public string Rg { get; set; } = string.Empty;
    public ICollection<FamiliaUsuario> FamiliaUsuarios { get; set; } = [];
    public DateOnly DataNascimento { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Nis { get; set; } = string.Empty;
    public string Ocupacao { get; set; } = string.Empty;
    public float RendaBruta { get; set; } = 0;
    public string Profissao { get; set; } = string.Empty;
    public OrientacaoSexualEnum OrientacaoSexual { get; set; }
    public RacaEnum Raca { get; set; }
    public EstadoCivilEnum EstadoCivil { get; set; }
    public EscolaridadeEnum Escolaridade { get; set; }
    public SexoEnum Sexo { get; set; }
    public FonteRendaEnum FonteRenda { get; set; }
}
