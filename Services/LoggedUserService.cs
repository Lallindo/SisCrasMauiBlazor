using SisCras.Models;

namespace SisCras.Services;

public class LoggedUserService : ILoggedUserService
{
    public Tecnico? CurrentUser { get; private set; }
    public bool IsUserLoggedIn => CurrentUser != null;

    public event Action? UserStateChanged;

    public Tecnico? GetCurrentUser()
    {
        return CurrentUser;
    }

    public void SetCurrentUser(Tecnico? tecnico)
    {
        CurrentUser = tecnico;
        UserStateChanged?.Invoke();
    }

    public void ClearCurrentUser()
    {
        CurrentUser = null;
        UserStateChanged?.Invoke();
    }
}