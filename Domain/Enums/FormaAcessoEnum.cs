using System.ComponentModel;

namespace SisCras.Domain.Entities.Enums;

public enum FormaAcessoEnum
{
    [Description("Demanda Espontânea")] Espontanea,

    [Description("Busca ativa realizada pela equipe do CRAS")]
    BuscaAtiva,

    [Description("Em decorrência de encaminhamento por outros serviços/unidades da PSB")]
    EncaminhamentoPsb,

    [Description("Em decorrência de encaminhamento por outros serviços/unidades da PSE")]
    EncaminhamentoPse,

    [Description("Em decorrência de encaminhamento realizado pela área da saúde")]
    EncaminhamentoSaude,

    [Description("Em decorrência de encaminhamento realizado pela área da educação")]
    EncaminhamentoEducacao,

    [Description("Em decorrência de encaminhamento realizado por outras políticas setoriais")]
    EncaminhamentoOutro,

    [Description("Em decorrência de encaminhamento realizado pelo Conselho Tutelar")]
    EncaminhamentoTutelar,

    [Description("Em decorrência de encaminhamento realizado pelo poder judiciário")]
    EncaminhamentoJudiciario,

    [Description("Em decorrência de encaminhamento realizado por outros orgãos do SGD")]
    EncaminhamentoSgd,
    Outros
}