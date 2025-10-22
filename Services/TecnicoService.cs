using System.Diagnostics;
using SisCras.Models;
using SisCras.Models.ValueObjects;
using SisCras.Repositories;

namespace SisCras.Services;

public class TecnicoService(ITecnicoRepository tecnicoRepository, ILoggedUserService loggedUserService, IPasswordService passwordService) : BaseService<Tecnico>(tecnicoRepository), ITecnicoService
{
    ITecnicoRepository _TecnicoRepository { get; } = tecnicoRepository;
    ILoggedUserService _LoggedUserService { get; } = loggedUserService;
    IPasswordService _PasswordService { get; } = passwordService;

    public async Task<bool> TryLoginAsync(string login, string plainSenha)
    {
        var tecnico = await _TecnicoRepository.GetTecnicoByLoginAsync(login);
        if (tecnico == null) return false;

        bool senhaCorreta = _PasswordService.VerifyPassword(plainSenha, new(tecnico.Senha));
        
        if (senhaCorreta)
        {
            tecnico.SetCrasInfo(await _TecnicoRepository.GetCurrentCrasByIdAsync(tecnico.Id));
            _LoggedUserService.SetCurrentUser(tecnico);
            return true;
        }
        
        return false;
    }
}