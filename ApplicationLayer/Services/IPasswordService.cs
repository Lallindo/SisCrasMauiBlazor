using SisCras.Domain.ValueObjects;
using SisCras.Domain.ValueObjects;

namespace SisCras.ApplicationLayer.Services;

public interface IPasswordService
{
    PasswordHash CreatePassword(string plainPassword);
    bool VerifyPassword(string plainPassword, PasswordHash passwordHash);
}