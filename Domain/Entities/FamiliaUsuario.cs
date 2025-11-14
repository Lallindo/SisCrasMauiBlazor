using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Domain.Enums;

namespace SisCras.Domain.Entities;

public class FamiliaUsuario
{
    public Familia? Familia { get; set; }
    public ParentescoEnum Parentesco { get; set; }
    public Usuario? Usuario { get; set; }
    public DateOnly DataAdicao { get; set; }
    public DateOnly? DataSaida { get; set; }

    public bool Ativo => DataSaida == null;
    public int Id { get; set; }
    public int FamiliaId { get; set; }
    public int UsuarioId { get; set; }
}