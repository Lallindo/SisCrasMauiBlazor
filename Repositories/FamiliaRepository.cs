using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class FamiliaRepository(SisCrasDbContext dbContext) : EfRepository<Familia>(dbContext), IFamiliaRepository
{
    
}