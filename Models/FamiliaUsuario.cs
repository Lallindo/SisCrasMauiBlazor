using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class FamiliaUsuario : ObservableObject
{
    [ObservableProperty]
    private bool _ativo = true;

    [ObservableProperty]
    private Familia? _familia;
    [ObservableProperty]
    private ParentescoEnum _parentesco;
    [ObservableProperty]
    private Usuario? _usuario;
    public int Id { get; set; }
    public int FamiliaId { get; set; }
    public int UsuarioId { get; set; }
}