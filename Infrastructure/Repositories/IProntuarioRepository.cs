using SisCras.Domain.Entities;

namespace SisCras.Infrastructure.Repositories;

public interface IProntuarioRepository : IRepository<Prontuario>
{
    Task<Prontuario> GetFamiliaFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaFromProntuario(int id);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario);
    Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(int id);
}