using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Entities.Enums;

namespace SisCras.Domain.Entities;

public partial class FamiliaUsuario
{
    public Familia? Familia { get; set; }
    public ParentescoEnum Parentesco;
    public Usuario? Usuario;
    public DateOnly DataAdicao;
    public DateOnly? DataSaida;

    public bool Ativo => DataSaida == null;
    public int Id { get; set; }
    public int FamiliaId { get; set; }
    public int UsuarioId { get; set; }
}