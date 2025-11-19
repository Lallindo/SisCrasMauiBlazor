using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.ValueObjects;

using CrasInfo = SisCras.Domain.ValueObjects.CrasInfo;

namespace SisCras.Domain.Entities;

public class Tecnico
{
    public int Id { get; set; } = 0;
    public string Login { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public ICollection<Prontuario> Prontuarios { get; set; } = [];
    public string Senha { get; set; } = string.Empty;
    public ICollection<TecnicoCras> TecnicoCras { get; set; } = [];

    public Cras? CrasAtivo
    {
        get
        {
            return TecnicoCras?.FirstOrDefault(tc => tc.DataSaida == null)?.Cras;
        }
    }

    [NotMapped]
    public bool IsAdmin
    {
        get
        {
            return TecnicoCras?.Any(tc => tc.DataSaida == null && tc.Admin) ?? false;
        }
    }
}