using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.ValueObjects;
using SisCras.Services;

namespace SisCras.Models;

public partial class Tecnico : ObservableObject
{   
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public ICollection<Prontuario> Prontuarios { get; set; }
    public ICollection<TecnicoCras> TecnicoCras { get; set; }
    public CrasInfo CrasInfo { get; set; }

    public void ChangeSenhaForHash(string plainSenha, IPasswordService passwordService)
    {
        var hash = PasswordHash.Create(plainSenha);
        Senha = hash.Hash;
    }
    public void SetCrasInfo(int id, string nome)
    {
        CrasInfo = CrasInfo.Create(id, nome);
    }
    public void SetCrasInfo(CrasInfo crasInfo)
    {
        CrasInfo = crasInfo;
    }
}