using System.Diagnostics;
using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public class LoggedUserService : ILoggedUserService
{
    private Tecnico? CurrentUser { get; set; }
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