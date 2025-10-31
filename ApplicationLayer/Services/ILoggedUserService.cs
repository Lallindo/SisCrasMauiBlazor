using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface ILoggedUserService
{
    public Tecnico? GetCurrentUser();
    public void SetCurrentUser(Tecnico tecnico);
    public void ClearCurrentUser();
    event Action UserStateChanged;
}