using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class FamiliaUsuario : ObservableObject
{
    public int FamiliaId { get; set; }
    public int UsuarioId { get; set; }
    public Familia Familia { get; set; }
    public Usuario Usuario { get; set; }
    public bool Ativo { get; set; }
    public ParentescoEnum Parentesco { get; set; } 
}