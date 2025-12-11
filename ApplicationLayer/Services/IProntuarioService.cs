using SisCras.Domain.Entities;

namespace SisCras.ApplicationLayer.Services;

public interface IProntuarioService : IBaseService<Prontuario>
{
    Task<Prontuario> GetFamiliaFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaFromProntuario(int id);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(int id);
    Task<Prontuario?> GetProntuarioByFamiliaId(int familiaId);
    Task<Prontuario?> GetProntuarioByFamiliaId(Familia familia);
    Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(int familiaId);
    Task<Prontuario?> GetProntuarioByFamiliaIdNoTracking(Familia familia);
    Task<Prontuario?> ImportProntuario(Prontuario prontuario);
    Task<Prontuario?> ReactivateProntuario(int prontuarioId);
}
