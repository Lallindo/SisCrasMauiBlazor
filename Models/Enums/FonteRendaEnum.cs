using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum FonteRendaEnum
{
    [Description("Trabalho informal")]
    Trabalho_Informal,
    [Description("Trabalho formal")]
    Trabalho_Formal,
    Aposentadoria,
    [Description("Pensão por morte")]
    Pensao_Morte,
    [Description("Pensão alimentícia")]
    Pensao_Alimenticia,
    [Description("Auxílio doença")]
    Auxilio_Doenca,
    [Description("BPC - Pessoa idosa")]
    BPC_Idoso,
    [Description("BPC - Pessoa com deficiência")]
    BPC_Pcd,
    [Description("Doação")]
    Doacao,
    Nenhuma
}