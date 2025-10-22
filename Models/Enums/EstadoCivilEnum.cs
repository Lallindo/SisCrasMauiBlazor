using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum EstadoCivilEnum
{
    Casado,
    [Description("União Estável")]
    Uniao_Estavel,
    Amasiado,
    Separado,
    Divorciado,
    Outro,
    Solteiro
}