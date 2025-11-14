using System.ComponentModel;

namespace SisCras.Domain.Enums;

public enum ParentescoEnum
{
    [Description("Selecione um parentesco")]Default,
    [Description("Reponsável")] Responsavel,
    [Description("Cônjuge")] Conjuge,
    [Description("Filho(a)")] Filho
}