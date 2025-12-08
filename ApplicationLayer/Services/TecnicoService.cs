using SisCras.Domain.Entities;
using SisCras.Domain.ValueObjects;
using SisCras.Domain.ValueObjects;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class TecnicoService(
    ITecnicoRepository tecnicoRepository,
    ILoggedUserService loggedUserService,
    IPasswordService passwordService) : BaseService<Tecnico>(tecnicoRepository), ITecnicoService
{
    private ITecnicoRepository TecnicoRepository { get; } = tecnicoRepository;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    private IPasswordService PasswordService { get; } = passwordService;

    public async Task<bool> TryLoginAsync(string login, string plainSenha)
    {
        var tecnico = await TecnicoRepository.GetTecnicoByLogin(login);
        if (tecnico == null) return false;

        var senhaCorreta = PasswordService.VerifyPassword(plainSenha, tecnico.Senha);

        if (senhaCorreta)
        {
            LoggedUserService.SetCurrentUser(tecnico);
            return true;
        }

        return false;
    }

    public async Task<Tecnico> ChangeSenhaForHash(Tecnico tecnico, IPasswordService passwordService)
    {
        tecnico.Senha = Task.FromResult(passwordService.CreatePassword(tecnico.Senha)).Result;
        return tecnico;
    }
}
