using System.ComponentModel;

namespace SisCras.Models.Enums;

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