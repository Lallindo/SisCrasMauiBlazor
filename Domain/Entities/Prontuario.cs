using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Entities.Enums;

namespace SisCras.Domain.Entities;

public partial class Prontuario
{
    public int Id { get; set; } = 0;
    public int Codigo { get; set; } = 0;
    public Cras? Cras { get; set; }
    public Tecnico? Tecnico { get; set; }
    public Familia? Familia { get; set; }
    public DateOnly DataCriacao { get; set; }
    public DateOnly? DataSaida { get; set; } = null;
    public FormaAcessoEnum FormaDeAcesso { get; set; } = FormaAcessoEnum.Espontanea;

    public int TecnicoId { get; set; }
    public int FamiliaId { get; set; }
    public int CrasId { get; set; }
    public bool Ativo => DataSaida == null;
}