using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class Cras
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<TecnicoCras> TecnicosCras { get; set; }
    public ICollection<Prontuario> Prontuarios { get; set; }
}