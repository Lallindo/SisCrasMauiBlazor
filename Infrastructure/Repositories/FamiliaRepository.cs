using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;
using SisCras.Domain.Entities.Enums;
using SisCras.Infrastructure.Data.Context;

namespace SisCras.Infrastructure.Repositories;

public class FamiliaRepository(SisCrasDbContext dbContext) : BaseRepository<Familia>(dbContext), IFamiliaRepository
{
    public async Task<List<Usuario?>> GetActiveUsuariosFromFamilia(int id)
    {
        return await DbContext.Familias
            .Where(f => f.Id == id)
            .SelectMany(fu => fu.FamiliaUsuarios)
            .Where(fu => fu.DataSaida == null)
            .Select(u => u.Usuario)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Usuario?>> GetActiveUsuariosFromFamilia(Familia familia)
    {
        return await GetActiveUsuariosFromFamilia(familia.Id);
    }

    public async Task<List<Usuario?>> GetUsuariosFromFamilia(int id)
    {
        return await DbContext.Familias
            .Where(f => f.Id == id)
            .SelectMany(fu => fu.FamiliaUsuarios)
            .Select(u => u.Usuario)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Usuario?>> GetUsuariosFromFamilia(Familia familia)
    {
        return await GetUsuariosFromFamilia(familia.Id);
    }

    public async Task<Usuario?> GetResponsavelFromFamilia(int id)
    {
        return await DbContext.Familias
            .Where(f => f.Id == id)
            .SelectMany(fu => fu.FamiliaUsuarios)
            .Where(fu => fu.DataSaida == null && fu.Parentesco == ParentescoEnum.Responsavel)
            .Select(u => u.Usuario)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetResponsavelFromFamilia(Familia familia)
    {
        return await GetResponsavelFromFamilia(familia.Id);
    }
}