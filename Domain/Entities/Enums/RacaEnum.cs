using System.ComponentModel;

namespace SisCras.Domain.Entities.Enums;

public enum RacaEnum
{
    Negra,
    Branca,
    [Description("Indígena")]
    Indigena,
    Amarela,
    Parda
}