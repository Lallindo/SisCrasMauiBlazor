using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface ITecnicoService : IBaseService<Tecnico>
{
    Task<bool> TryLoginAsync(string login, string plainSenha);
    Task<Tecnico> ChangeSenhaForHash(Tecnico tecnico, IPasswordService passwordService);
}