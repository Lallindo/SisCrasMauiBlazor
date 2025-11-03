using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class ProntuarioRepository(SisCrasDbContext dbContext) : BaseRepository<Prontuario>(dbContext), IProntuarioRepository
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