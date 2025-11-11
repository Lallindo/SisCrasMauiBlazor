using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Entities.ValueObjects;

namespace SisCras.Domain.Entities;

public partial class Tecnico
{
    public CrasInfo CrasInfoProp { get; set; }
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

    public void SetCrasInfo(int id, string nome)
    {
        CrasInfoProp = CrasInfo.Create(id, nome);
    }

    public void SetCrasInfo(CrasInfo crasInfo)
    {
        CrasInfoProp = crasInfo;
    }
}