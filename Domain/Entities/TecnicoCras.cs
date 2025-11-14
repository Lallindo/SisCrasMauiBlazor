using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Domain.Entities;

public class TecnicoCras
{
    public Cras? Cras { get; set; } = null;
    public Tecnico? Tecnico { get; set; } = null;
    public DateOnly DataEntrada { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? DataSaida { get; set; } = null;
    public bool Admin { get; set; } = false;
    public int Id { get; set; }
    public int CrasId { get; set; }
    public int TecnicoId { get; set; }
    public bool Ativo => DataSaida == null;
}