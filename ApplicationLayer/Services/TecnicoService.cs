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

    public async Task<Tecnico?> TryLoginAsync(string login, string plainSenha)
    {
        var tecnico = await TecnicoRepository.GetTecnicoByLogin(login);
        if (tecnico == null) return null;

        var senhaCorreta = PasswordService.VerifyPassword(plainSenha, tecnico.Senha);

        if (senhaCorreta)
        {
            LoggedUserService.SetCurrentUser(tecnico);
            return tecnico;
        }

        return null;
    }

    public async Task<Tecnico> ChangeSenhaForHash(Tecnico tecnico, IPasswordService passwordService)
    {
        tecnico.Senha = Task.FromResult(passwordService.CreatePassword(tecnico.Senha)).Result;
        return tecnico;
    }

    public async Task<List<Tecnico>> GetAllTecnicos()
    {
        return await TecnicoRepository.GetAllTecnicos();
    }

    public async Task<Tecnico?> ImportTecnico(Tecnico tecnico)
    {
        var loggedTecnico = LoggedUserService.GetCurrentUser();

        if (loggedTecnico == null) return null;
        if (tecnico.IsAdmin) return null;
        
        tecnico.TecnicoCrasAtivo.DataSaida = DateOnly.FromDateTime(DateTime.Now);

        tecnico.TecnicoCras.Add(
            new()
            {
                Admin = false,
                CrasId = loggedTecnico.CrasAtivo.Id,
                DataEntrada = DateOnly.FromDateTime(DateTime.Now),
                DataSaida = null,
                TecnicoId = loggedTecnico.Id
            });

        await TecnicoRepository.UpdateAsync(tecnico);

        return tecnico;
    }

    public async Task<Tecnico?> ReactivateTecnico(Tecnico tecnico)
    {
        var loggedTecnico = LoggedUserService.GetCurrentUser();

        if (loggedTecnico == null) return null;
        if (tecnico.IsAdmin) return null;

        tecnico.TecnicoCras.Add(
            new()
            {
                Admin = false,
                CrasId = loggedTecnico.CrasAtivo.Id,
                DataEntrada = DateOnly.FromDateTime(DateTime.Now),
                DataSaida = null,
                TecnicoId = loggedTecnico.Id
            });

        await TecnicoRepository.UpdateAsync(tecnico);

        return tecnico;
    }
}
