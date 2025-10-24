namespace SisCras.Models.ValueObjects;

public class PasswordHash(string hash)
{
    public string Hash { get; } = hash;

    public static PasswordHash Create(string plainSenha)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(plainSenha);
        return new PasswordHash(hash);
    }
    public bool Verify(string plainSenha)
    {
        if (plainSenha == null) return false;
        return BCrypt.Net.BCrypt.Verify(plainSenha, Hash);
    }
}