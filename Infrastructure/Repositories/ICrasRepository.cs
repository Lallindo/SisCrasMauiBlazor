using SisCras.Domain.Entities;

namespace SisCras.Infrastructure.Repositories;

public interface ICrasRepository : IRepository<Cras>
{
    Task<List<Tecnico>> GetTecnicosFromCras(int id);
    Task<List<Tecnico>> GetTecnicosFromCras(Cras cras);
    Task<List<Familia>> GetFamiliasFromCras(int id);
    Task<List<Familia>> GetFamiliasFromCras(Cras cras);
    Task<List<Prontuario>> GetProntuariosFromCras(int id);
    Task<List<Prontuario>> GetProntuariosFromCras(Cras cras);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(int id);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(Cras cras);
    Task<List<Prontuario>> GetAllProntuariosAndFamiliaAndUsuarios(int offset = 0, int limit = 20);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(int id);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCrasNoTracking(Cras cras);
}
