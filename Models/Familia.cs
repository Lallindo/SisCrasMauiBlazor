using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class Familia : ObservableObject
{
    public int Id { get; set; }
    public ICollection<Prontuario> Prontuarios { get; set; } = [];
    public ICollection<FamiliaUsuario> FamiliaUsuarios { get; set; } = [];
    public ConfiguracaoFamiliarEnum ConfiguracaoFamiliar { get; set; }
}