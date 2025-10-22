using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.Models.ValueObjects;
using SisCras.Services;

namespace SisCras.Models;

public partial class Tecnico : ObservableObject
{   
    [ObservableProperty]
    int _Id;
    [ObservableProperty]
    string _Nome;
    [ObservableProperty]
    string _Login;
    [ObservableProperty]
    string _Senha;
    [ObservableProperty]
    ICollection<Prontuario> _Prontuarios;
    [ObservableProperty]
    ICollection<TecnicoCras> _TecnicoCras;
    [ObservableProperty]
    CrasInfo _CrasInfo;

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