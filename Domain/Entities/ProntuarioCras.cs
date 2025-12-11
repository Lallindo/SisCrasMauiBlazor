using SisCras.Domain.Entities;
using SisCras.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisCras.Domain.Entities;

public class ProntuarioCras
{
    public int Id { get; set; } = 0;
    public int ProntuarioId { get; set; }
    public virtual Prontuario Prontuario { get; set; } = null!;

    public int CrasId { get; set; }
    public virtual Cras Cras { get; set; } = null!;

    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; } 

    public int TecnicoResponsavelId { get; set; }
    public virtual Tecnico TecnicoResponsavel { get; set; } = null!;
    
    public FormaAcessoEnum FormaDeAcesso { get; set; } = FormaAcessoEnum.Espontanea;

    [NotMapped]
    public bool Ativo => DataSaida == null;
}
