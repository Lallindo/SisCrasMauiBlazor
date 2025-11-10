using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Prontuario : ObservableObject
{
    public int TecnicoId { get; set; }
    public int FamiliaId { get; set; }
    public int CrasId { get; set; }
    
    public int Id { get; set; }
    public int Codigo { get; set; }
    public Tecnico Tecnico { get; set; }
    public Familia Familia { get; set; }
    public Cras Cras { get; set; }
    public string FormaDeAcesso { get; set; }
    public DateOnly DataCriacao { get; set; }
}
