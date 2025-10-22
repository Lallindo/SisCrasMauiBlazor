using SisCras.Database;
using SisCras.Models;

namespace SisCras.Repositories;

public class UsuarioRepository(SisCrasDbContext dbContext) : EfRepository<Usuario>(dbContext), IUsuarioRepository
{
    
}