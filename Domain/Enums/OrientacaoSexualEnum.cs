using System.ComponentModel;

namespace SisCras.Domain.Enums;

public enum OrientacaoSexualEnum
{
    [Description("Selecione uma orientação sexual")]Default,
    Heterossexual,
    Homossexual,
    Bissexual,
    Outro
}