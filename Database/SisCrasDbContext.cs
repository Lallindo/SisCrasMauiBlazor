using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SisCras.Models;

namespace SisCras.Database;

public partial class SisCrasDbContext(DbContextOptions<SisCrasDbContext> options) : DbContext(options)
{
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Prontuario> Prontuarios { get; set; }
    public DbSet<Familia> Familias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<FamiliaUsuario> FamiliaUsuarios { get; set; }
    public DbSet<Cras> Cras { get; set; }
    public DbSet<TecnicoCras> TecnicoCras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={GetSqLiteConnection()}");

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

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.Ignore(t => t.CrasInfo);
        });

        modelBuilder.Entity<Prontuario>(entity =>
        {
            entity.HasOne(p => p.Tecnico)
                .WithMany(t => t.Prontuarios)
                .HasForeignKey(p => p.TecnicoId)  
                .IsRequired();

            entity.HasOne(p => p.Familia)
                .WithMany(f => f.Prontuarios)
                .HasForeignKey(p => p.FamiliaId)
                .IsRequired();

            entity.Ignore(p => p.Ativo);
        });

        modelBuilder.Entity<Familia>(entity =>
        {
            entity.Property(f => f.ConfiguracaoFamiliar).HasConversion<int>();
        });

        modelBuilder.Entity<FamiliaUsuario>(entity =>
        {
            entity.HasKey(fu => new { fu.Id });
            
            entity.Property(fu => fu.Parentesco ).HasConversion<int>();
            
            entity.HasOne(fu => fu.Familia)
                .WithMany(f => f.FamiliaUsuarios)
                .HasForeignKey(fu => fu.FamiliaId)
                .IsRequired();

            entity.HasOne(fu => fu.Usuario)
                .WithMany(u => u.FamiliaUsuarios)
                .HasForeignKey(fu => fu.UsuarioId)
                .IsRequired();
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
        });
    }

    private static string GetSqLiteConnection()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "siscras.db");
        return dbPath;
    }
}