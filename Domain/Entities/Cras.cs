using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Domain.Entities;

public partial class Cras
{
    public int Id { get; set; } = 0;
    public string Nome { get; set; } = string.Empty;
    public ICollection<Prontuario> Prontuarios { get; set; } = [];
    public ICollection<TecnicoCras> TecnicosCras { get; set; } = [];
}