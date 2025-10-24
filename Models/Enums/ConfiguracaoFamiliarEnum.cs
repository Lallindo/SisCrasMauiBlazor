using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum ConfiguracaoFamiliarEnum
{
    [Description("Casal sem filho(s)")]
    CasalSFilhos,
    [Description("Casal sem filho(s) e com parente(s)")]
    CasalSFilhosCParentes,
    [Description("Família nuclear")]
    FamiliaNuclear,
    [Description("Família nuclear com parente(s)")]
    FamiliaNuclearCParentes,
    [Description("Monoparental feminina")]
    MonoparentalFeminina,
    [Description("Monoparental feminina com parente(s)")]
    MonoparentalFemininaCParentes,
    [Description("Monoparental masculina")]
    MonoparentalMasculina,
    [Description("Monoparental masculina com parente(s)")]
    MonoparentalMasculinaCParentes,
    [Description("Avós com netos")]
    AvosCNetos,
    [Description("Reconstituída")]
    Reconstituida,
    [Description("Anaparental")]
    Anaparental,
    [Description("Multiespécie")]
    Multiespecie,
    [Description("Família extensa")]
    FamiliaExtensa,
    [Description("Família unipessoal feminina")]
    FamiliaUnipessoalFem,
    [Description("Família unipessoal masculina")]
    FamiliaUnipessoalMas,
    [Description("Família homoafetiva")]
    FamiliaHomoafetiva,
    [Description("Família eudemonista")]
    FamiliaEudemonista,
    [Description("Família socioafetiva")]
    FamiliaSocioafetiva
}