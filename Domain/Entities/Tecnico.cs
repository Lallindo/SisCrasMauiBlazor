using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.ValueObjects;

using CrasInfo = SisCras.Domain.ValueObjects.CrasInfo;

namespace SisCras.Domain.Entities;

public class Tecnico
{
    public CrasInfo CrasAtivo { get; set; }
    public int Id { get; set; } = 0;
    public string Login { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public ICollection<Prontuario> Prontuarios { get; set; } = [];
    public string Senha { get; set; }
    public ICollection<TecnicoCras> TecnicoCras { get; set; } = [];
    
    public void ChangeSenhaForHash(string plainSenha, IPasswordService passwordService)
    {
        var hash = PasswordHash.Create(plainSenha);
        Senha = hash.Hash;
    }

    public void SetCrasAtivo(int crasId, string crasNome)
    {
        CrasAtivo = new(crasId, crasNome);
    }

    public void SetCrasAtivo(Cras cras)
    {
        CrasAtivo = new(cras.Id, cras.Nome);
    }

    public void SetCrasAtivo(CrasInfo cras)
    {
        CrasAtivo = cras;
    }
}