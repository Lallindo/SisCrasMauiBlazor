using System.Diagnostics;
using SisCras.Models;
using SisCras.Models.ValueObjects;
using SisCras.Repositories;

namespace SisCras.Services;

public class TecnicoService(ITecnicoRepository tecnicoRepository, ILoggedUserService loggedUserService, IPasswordService passwordService) : BaseService<Tecnico>(tecnicoRepository), ITecnicoService
{
    ITecnicoRepository TecnicoRepository { get; } = tecnicoRepository;
    ILoggedUserService LoggedUserService { get; } = loggedUserService;
    IPasswordService PasswordService { get; } = passwordService;

    public async Task<bool> TryLoginAsync(string login, string plainSenha)
    {
        var tecnico = await TecnicoRepository.GetTecnicoByLoginAsync(login);
        if (tecnico == null) return false;

        bool senhaCorreta = PasswordService.VerifyPassword(plainSenha, new(tecnico.Senha));
        
        if (senhaCorreta)
        {
            tecnico.SetCrasInfo(await TecnicoRepository.GetCurrentCrasByIdAsync(tecnico.Id));
            LoggedUserService.SetCurrentUser(tecnico);
            return true;
        }
        
        return false;
    }
}