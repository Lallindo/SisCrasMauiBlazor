using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class ProntuarioRepository(SisCrasDbContext dbContext) : EfRepository<Prontuario>(dbContext), IProntuarioRepository
{
    
}