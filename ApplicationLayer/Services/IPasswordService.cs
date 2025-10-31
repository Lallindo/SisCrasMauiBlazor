using SisCras.Domain.Entities.ValueObjects;

namespace SisCras.ApplicationLayer.Services;

public interface IPasswordService
{
    PasswordHash CreatePassword(string plainPassword);
    bool VerifyPassword(string plainPassword, PasswordHash passwordHash);
}