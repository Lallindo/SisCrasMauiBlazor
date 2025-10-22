using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum RacaEnum
{
    Negra,
    Branca,
    [Description("Indígena")]
    Indigena,
    Amarela,
    Parda
}