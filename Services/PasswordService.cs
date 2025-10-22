using SisCras.Models.ValueObjects;

namespace SisCras.Services;

public class PasswordService : IPasswordService
{
    public PasswordHash CreatePassword(string plainPassword)
    {
        return PasswordHash.Create(plainPassword);
    }
    public bool VerifyPassword(string plainPassword, PasswordHash hashedPassword)
    {
        return hashedPassword.Verify(plainPassword);
    }
}