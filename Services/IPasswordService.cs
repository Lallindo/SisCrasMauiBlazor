using SisCras.Models.ValueObjects;

namespace SisCras.Services;

public interface IPasswordService
{
    PasswordHash CreatePassword(string plainPassword);
    bool VerifyPassword(string plainPassword, PasswordHash passwordHash);
}