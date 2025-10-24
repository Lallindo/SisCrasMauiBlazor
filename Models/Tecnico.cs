using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.ValueObjects;
using SisCras.Services;

namespace SisCras.Models;

public partial class Tecnico : ObservableObject
{
    [ObservableProperty]
    private CrasInfo _crasInfo;
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _login;
    [ObservableProperty]
    private string _nome;
    [ObservableProperty]
    private ICollection<Prontuario> _prontuarios;
    [ObservableProperty]
    private string _senha;
    [ObservableProperty]
    private ICollection<TecnicoCras> _tecnicoCras;

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