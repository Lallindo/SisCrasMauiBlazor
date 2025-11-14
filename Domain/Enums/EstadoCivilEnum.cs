using System.ComponentModel;

namespace SisCras.Domain.Enums;

public enum EstadoCivilEnum
{
    [Description("Selecione um estado civil")]Default,
    Casado,
    [Description("União Estável")] UniaoEstavel,
    Amasiado,
    Separado,
    Divorciado,
    Outro,
    Solteiro
}