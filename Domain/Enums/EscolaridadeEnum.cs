using System.ComponentModel;

namespace SisCras.Domain.Enums;

public enum EscolaridadeEnum
{
    [Description("Selecione uma escolaridade")]Default,
    [Description("Sem escolaridade")] SemEscolaridade,

    [Description("Fundamental incompleto")]
    FundIncompleto,
    [Description("Fundamental completo")] FundCompleto,

    [Description("Ensino médio incompleto")]
    MedIncompleto,
    [Description("Ensino médio completo")] MedCompleto,
    [Description("Superior incompleto")] SupIncompleto,
    [Description("Superior Completo")] SupCompleto,
    [Description("Pós-Graduado")] PosGraduado,
    Outra
}