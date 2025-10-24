using SisCras.Models;

namespace SisCras.Services;

public interface ICrasService : IBaseService<Cras>
{
    Task<List<Tecnico>> GetTecnicosFromCras(int id);
    Task<List<Tecnico>> GetTecnicosFromCras(Cras cras);
    Task<List<Familia>> GetFamiliasFromCras(int id);
    Task<List<Familia>> GetFamiliasFromCras(Cras cras);
    Task<List<Prontuario>> GetProntuariosFromCras(int id);
    Task<List<Prontuario>> GetProntuariosFromCras(Cras cras);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(int id);
    Task<List<Prontuario>> GetProntuarioAndFamiliaAndUsuariosFromCras(Cras cras);
}