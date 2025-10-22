using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum EscolaridadeEnum
{
    [Description("Sem escolaridade")]
    Sem_Escolaridade,
    [Description("Fundamental incompleto")]
    Fund_Incompleto,
    [Description("Fundamental completo")]
    Fund_Completo,
    [Description("Ensino médio incompleto")]
    Med_Incompleto,
    [Description("Ensino médio completo")]
    Med_Completo,
    [Description("Superior incompleto")]
    Sup_Incompleto,
    [Description("Superior Completo")]
    Sup_Completo,
    [Description("Pós-Graduado")]
    Pos_Graduado,
    Outra
}