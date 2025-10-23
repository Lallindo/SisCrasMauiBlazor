using SisCras.Models;

namespace SisCras.Repositories;

public interface ICrasRepository : IRepository<Cras>
{
    Task<List<Tecnico>> GetTecnicosFromCras(Cras cras);
    Task<List<Tecnico>> GetTecnicosFromCras(int id);
    Task<List<Familia>> GetFamiliasFromCras(Cras cras);
    Task<List<Familia>> GetFamiliasFromCras(int id);
    Task<List<Prontuario>> GetProntuariosFromCras(Cras cras);
    Task<List<Prontuario>> GetProntuariosFromCras(int id);
}