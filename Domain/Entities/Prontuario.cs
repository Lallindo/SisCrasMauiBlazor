using SisCras.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisCras.Domain.Entities;

public class Prontuario
{
    public int Id { get; set; } = 0;
    public int Codigo { get; set; } = 0;
    
    public int FamiliaId { get; set; }
    public virtual Familia? Familia { get; set; }
    
    // "Cache" do local atual para facilitar as telas de listagem
    public int CrasId { get; set; }
    public virtual Cras? Cras { get; set; }
    
    public int TecnicoId { get; set; }
    public virtual Tecnico? Tecnico { get; set; }
    
    public FormaAcessoEnum FormaDeAcesso { get; set; } // Opcional: mantido para compatibilidade rápida

    // A Nova Lista de Histórico
    public virtual ICollection<ProntuarioCras> HistoricoCras { get; set; } = new List<ProntuarioCras>();

    // Propriedade auxiliar para pegar o histórico ativo facilmente
    [NotMapped]
    public ProntuarioCras? ProntuarioAtivo
    {
        get
        {
            return HistoricoCras?.FirstOrDefault(pc => pc.DataSaida == null);
        }
    }
    
    // Removemos a propriedade DataSaida antiga, pois agora é calculada pelo histórico
    [NotMapped]
    public DateTime? DataSaida => ProntuarioAtivo == null ? DateTime.Now : null; 
}
