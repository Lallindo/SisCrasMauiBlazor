using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Models;

public partial class FamiliaUsuario : ObservableObject
{
    public int FamiliaId { get; set; }
    public int UsuarioId { get; set; }

    [ObservableProperty]
    Familia _Familia;
    [ObservableProperty]
    Usuario _Usuario;
    [ObservableProperty]
    bool _Ativo;
    [ObservableProperty]
    string _Parentesco; // TODO Deve ser alterada por Enum 
}