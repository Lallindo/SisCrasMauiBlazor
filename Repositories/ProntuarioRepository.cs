using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class ProntuarioRepository(SisCrasDbContext dbContext) : EfRepository<Prontuario>(dbContext), IProntuarioRepository
{
    public Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Prontuario> GetFamiliaAndUsuariosFromProntuario(Prontuario prontuario)
    {
        throw new NotImplementedException();
    }

    public Task<Prontuario> GetFamiliaFromProntuario(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Prontuario> GetFamiliaFromProntuario(Prontuario prontuario)
    {
        throw new NotImplementedException();
    }
}