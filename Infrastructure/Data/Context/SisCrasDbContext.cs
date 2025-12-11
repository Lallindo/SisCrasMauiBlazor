using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities;

namespace SisCras.Infrastructure.Data.Context;

public class SisCrasDbContext : DbContext
{
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Prontuario> Prontuarios { get; set; }
    // NOVA TABELA
    public DbSet<ProntuarioCras> ProntuarioCras { get; set; } 
    public DbSet<Familia> Familias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<FamiliaUsuario> FamiliaUsuarios { get; set; }
    public DbSet<Cras> Cras { get; set; }
    public DbSet<TecnicoCras> TecnicoCras { get; set; }

    public SisCrasDbContext(DbContextOptions<SisCrasDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(u => u.Sexo).HasConversion<int>();
            entity.Property(u => u.EstadoCivil).HasConversion<int>();
            entity.Property(u => u.OrientacaoSexual).HasConversion<int>();
            entity.Property(u => u.Raca).HasConversion<int>();
            entity.Property(u => u.Escolaridade).HasConversion<int>();
            entity.Property(u => u.FonteRenda).HasConversion<int>();
        });

        modelBuilder.Entity<Tecnico>(entity => { entity.Ignore(t => t.CrasAtivo); });

        modelBuilder.Entity<Prontuario>(entity =>
        {
            // Relacionamentos do "Cache" (Estado Atual)
            entity.HasOne(p => p.Tecnico)
                .WithMany(t => t.Prontuarios)
                .HasForeignKey(p => p.TecnicoId)
                .IsRequired();

            entity.HasOne(p => p.Cras)
                .WithMany(c => c.Prontuarios)
                .HasForeignKey(p => p.CrasId)
                .IsRequired();

            entity.HasOne(p => p.Familia)
                .WithMany(f => f.Prontuarios)
                .HasForeignKey(p => p.FamiliaId)
                .IsRequired();

            // Configurações de propriedades
            entity.Property(p => p.FormaDeAcesso).HasConversion<int>();
            
            // Ignorar propriedades calculadas que não vão pro banco na tabela Pai
            // (Note que DataSaida agora é calculada, então garantimos que o EF ignore se não houver atributo [NotMapped] na classe)
            entity.Ignore(p => p.ProntuarioAtivo);
        });

        // --- CONFIGURAÇÃO DA NOVA TABELA DE HISTÓRICO ---
        modelBuilder.Entity<ProntuarioCras>(entity =>
        {
            // Chave Primária (BaseEntity tem Id, então usamos ele)
            entity.HasKey(pc => pc.Id);

            // Relacionamento com o Prontuário Pai
            entity.HasOne(pc => pc.Prontuario)
                .WithMany(p => p.HistoricoCras) // Liga com a coleção na classe Prontuario
                .HasForeignKey(pc => pc.ProntuarioId)
                .IsRequired();

            // Relacionamento com o CRAS onde ocorreu o vínculo
            entity.HasOne(pc => pc.Cras)
                .WithMany() // Não precisamos de uma lista de históricos dentro da classe CRAS por enquanto
                .HasForeignKey(pc => pc.CrasId)
                .IsRequired();

            // Relacionamento com o Técnico que fez a movimentação
            entity.HasOne(pc => pc.TecnicoResponsavel)
                .WithMany()
                .HasForeignKey(pc => pc.TecnicoResponsavelId)
                .IsRequired();

            // Conversão do Enum
            entity.Property(pc => pc.FormaDeAcesso).HasConversion<int>();
        });

        modelBuilder.Entity<Familia>(entity =>
        {
            entity.Property(f => f.ConfiguracaoFamiliar).HasConversion<int>();
            entity.Ignore(f => f.Usuarios);
        });

        modelBuilder.Entity<FamiliaUsuario>(entity =>
        {
            entity.HasKey(fu => new { fu.Id });
            entity.Property(fu => fu.Parentesco).HasConversion<int>();

            entity.HasOne(fu => fu.Familia)
                .WithMany(f => f.FamiliaUsuarios)
                .HasForeignKey(fu => fu.FamiliaId)
                .IsRequired();

            entity.HasOne(fu => fu.Usuario)
                .WithMany(u => u.FamiliaUsuarios)
                .HasForeignKey(fu => fu.UsuarioId)
                .IsRequired();

            entity.Ignore(fu => fu.Ativo);
        });

        modelBuilder.Entity<TecnicoCras>(entity =>
        {
            entity.HasKey(tc => new { tc.Id });

            entity.HasOne(tc => tc.Cras)
                .WithMany(c => c.TecnicosCras)
                .HasForeignKey(tc => tc.CrasId)
                .IsRequired();

            entity.HasOne(tc => tc.Tecnico)
                .WithMany(t => t.TecnicoCras)
                .HasForeignKey(tc => tc.TecnicoId)
                .IsRequired();

            entity.Ignore(tc => tc.Ativo);
        });
    }

    public static string GetSqLiteConnection()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "siscras.db");
        return dbPath;
    }
}
