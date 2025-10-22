using SisCras.Models;

namespace SisCras.Services;

public interface ITecnicoService : IBaseService<Tecnico>
{
    Task<bool> TryLoginAsync(string login, string plainSenha);
}