using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.Domain.Entities.ValueObjects;

public partial class CrasInfo(int id, string nome) : ObservableObject
{
    public int Id { get; set; } = 0;
    public string Nome { get; set; } = "";

    public static CrasInfo Create(int id, string nome)
    {
        if (id <= 0)
            throw new ArgumentException("Id não existe");

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é inválido");

        return new CrasInfo(id, nome);
    }
}