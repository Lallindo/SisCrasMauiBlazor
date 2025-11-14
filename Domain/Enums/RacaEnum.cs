using System.ComponentModel;

namespace SisCras.Domain.Enums;

public enum RacaEnum
{
    [Description("Selecione uma raça")]Default,
    Negra,
    Branca,
    [Description("Indígena")] Indigena,
    Amarela,
    Parda
}