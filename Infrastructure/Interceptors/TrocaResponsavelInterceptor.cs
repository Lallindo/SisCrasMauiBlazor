using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

namespace SisCras.Infrastructure.Interceptors;

public class TrocaResponsavelInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        // 1. Detectar responsáveis que estão sendo desligados (DataSaida foi alterada para não-nulo)
        var responsaveisSaindo = context.ChangeTracker.Entries<FamiliaUsuario>()
            .Where(e => e.State == EntityState.Modified)
            .Where(e => e.Entity.Parentesco == ParentescoEnum.Responsavel)
            .Where(e => e.Entity.DataSaida != null)
            .Where(e => e.Property(x => x.DataSaida).IsModified)
            .ToList();

        foreach (var entry in responsaveisSaindo)
        {
            var familiaId = entry.Entity.FamiliaId;
            var usuarioSaindoId = entry.Entity.Id;

            // 2. Buscar substituto no banco (o membro ativo mais velho)
            // Usamos o próprio contexto para buscar, garantindo que tudo rode na mesma transação
            var novoResponsavel = await context.Set<FamiliaUsuario>()
                .Include(fu => fu.Usuario)
                .Where(fu => fu.FamiliaId == familiaId)
                .Where(fu => fu.DataSaida == null) // Apenas ativos
                .Where(fu => fu.Id != usuarioSaindoId) // Ignora quem está saindo
                .OrderBy(fu => fu.Usuario!.DataNascimento) // Regra: Mais velho assume
                .FirstOrDefaultAsync(cancellationToken);

            // 3. Promover o substituto
            if (novoResponsavel != null)
            {
                novoResponsavel.Parentesco = ParentescoEnum.Responsavel;
                
                // Marca explicitamente como modificado para garantir o Update
                context.Entry(novoResponsavel).State = EntityState.Modified;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
