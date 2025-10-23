using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class FamiliaRepository(SisCrasDbContext dbContext) : EfRepository<Familia>(dbContext), IFamiliaRepository
{
    public Task<List<Usuario>> GetActiveUsuariosFromFamilia(Familia familia)
    {
        throw new NotImplementedException();
    }

    public Task<List<Usuario>> GetActiveUsuariosFromFamilia(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Usuario>> GetUsuariosFromFamilia(Familia familia)
    {
        throw new NotImplementedException();
    }

    public Task<List<Usuario>> GetUsuariosFromFamilia(int id)
    {
        throw new NotImplementedException();
    }
}