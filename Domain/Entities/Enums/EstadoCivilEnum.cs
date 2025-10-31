using System.ComponentModel;

namespace SisCras.Domain.Entities.Enums;

public enum EstadoCivilEnum
{
    Casado,
    [Description("União Estável")]
    UniaoEstavel,
    Amasiado,
    Separado,
    Divorciado,
    Outro,
    Solteiro
}