using SisCras.Models;

namespace SisCras.Services;

public interface IProntuarioService : IBaseService<Prontuario>
{
    Task<Prontuario> GetFamiliaFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaFromProntuario(int id);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(int id);
}