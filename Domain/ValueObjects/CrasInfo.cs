namespace SisCras.Domain.ValueObjects;

public class CrasInfo(int id, string nome)
{
    public int Id { get; set; } = id;
    public string Nome { get; set; } = nome;

    public static CrasInfo Create(int id, string nome)
    {
        if (id <= 0)
            throw new ArgumentException("Id não existe");

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é inválido");

        return new CrasInfo(id, nome);
    }
}