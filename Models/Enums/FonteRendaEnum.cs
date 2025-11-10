using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum FonteRendaEnum
{
    [Description("Trabalho informal")]
    TrabalhoInformal,
    [Description("Trabalho formal")]
    TrabalhoFormal,
    Aposentadoria,
    [Description("Pensão por morte")]
    PensaoMorte,
    [Description("Pensão alimentícia")]
    PensaoAlimenticia,
    [Description("Auxílio doença")]
    AuxilioDoenca,
    [Description("BPC - Pessoa idosa")]
    BpcIdoso,
    [Description("BPC - Pessoa com deficiência")]
    BpcPcd,
    [Description("Doação")]
    Doacao,
    Nenhuma
}