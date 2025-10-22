using System.ComponentModel;

namespace SisCras.Models.Enums;

public enum ConfiguracaoFamiliarEnum
{
    [Description("Casal sem filho(s)")]
    Casal_S_Filhos,
    [Description("Casal sem filho(s) e com parente(s)")]
    Casal_S_Filhos_C_Parentes,
    [Description("Família nuclear")]
    Familia_Nuclear,
    [Description("Família nuclear com parente(s)")]
    Familia_Nuclear_C_Parentes,
    [Description("Monoparental feminina")]
    Monoparental_Feminina,
    [Description("Monoparental feminina com parente(s)")]
    Monoparental_Feminina_C_Parentes,
    [Description("Monoparental masculina")]
    Monoparental_Masculina,
    [Description("Monoparental masculina com parente(s)")]
    Monoparental_Masculina_C_Parentes,
    [Description("Avós com netos")]
    Avos_C_Netos,
    [Description("Reconstituída")]
    Reconstituida,
    [Description("Anaparental")]
    Anaparental,
    [Description("Multiespécie")]
    Multiespecie,
    [Description("Família extensa")]
    Familia_Extensa,
    [Description("Família unipessoal feminina")]
    Familia_Unipessoal_Fem,
    [Description("Família unipessoal masculina")]
    Familia_Unipessoal_Mas,
    [Description("Família homoafetiva")]
    Familia_Homoafetiva,
    [Description("Família eudemonista")]
    Familia_Eudemonista,
    [Description("Família socioafetiva")]
    Familia_Socioafetiva
}