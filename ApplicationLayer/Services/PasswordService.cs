using SisCras.Domain.Entities.ValueObjects;

namespace SisCras.ApplicationLayer.Services;

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