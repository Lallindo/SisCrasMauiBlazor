using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.Enums;

namespace SisCras.Models;

public partial class FamiliaUsuario : ObservableObject
{
    public int Id { get; private set; }
    public int FamiliaId { get; }
    public int UsuarioId { get; }

    [ObservableProperty]
    private Familia? _familia;
    [ObservableProperty]
    private Usuario? _usuario;
    [ObservableProperty]
    private bool _ativo = true;
    [ObservableProperty]
    private ParentescoEnum _parentesco;
}