using System.ComponentModel;

namespace SisCras.Domain.Entities.Enums;

public enum ParentescoEnum
{
    [Description("Reponsável")] Responsavel,
    [Description("Cônjuge")] Conjuge,
    [Description("Filho(a)")] Filho
}