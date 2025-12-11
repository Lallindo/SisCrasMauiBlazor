using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface ITecnicoService : IBaseService<Tecnico>
{
    Task<Tecnico?> TryLoginAsync(string login, string plainSenha);
    Task<Tecnico> ChangeSenhaForHash(Tecnico tecnico, IPasswordService passwordService);
    Task<List<Tecnico>> GetAllTecnicos();
    Task<Tecnico?> ImportTecnico(Tecnico tecnico);
    Task<Tecnico?> ReactivateTecnico(Tecnico tecnico);
}
