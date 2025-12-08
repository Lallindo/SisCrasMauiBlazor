using SisCras.Domain.ValueObjects;
using SisCras.Domain.ValueObjects;

namespace SisCras.ApplicationLayer.Services;

public interface IPasswordService
{
    string CreatePassword(string plainPassword);
    bool VerifyPassword(string plainPassword, string passwordHash);
}
