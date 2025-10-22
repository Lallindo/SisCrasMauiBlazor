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

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={GetSQLiteConnection()}");

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
            // Relationship with Tecnico
            entity.HasOne(p => p.Tecnico)
                .WithMany(t => t.Prontuarios)
                .HasForeignKey(p => p.TecnicoId)  // Changed from Id to TecnicoId
                .IsRequired();

            // Relationship with Familia
            entity.HasOne(p => p.Familia)
                .WithMany(f => f.Prontuarios)
                .HasForeignKey(p => p.FamiliaId)
                .IsRequired();
        });

        modelBuilder.Entity<Familia>(entity =>
        {
            entity.Property(f => f.ConfiguracaoFamiliar).HasConversion<int>();
        });

        modelBuilder.Entity<FamiliaUsuario>(entity =>
        {
            // Composite primary key
            entity.HasKey(fu => new { fu.FamiliaId, fu.UsuarioId });

            // Relationship with Familia
            entity.HasOne(fu => fu.Familia)
                .WithMany(f => f.FamiliaUsuarios)
                .HasForeignKey(fu => fu.FamiliaId)
                .IsRequired();

            // Relationship with Usuario
            entity.HasOne(fu => fu.Usuario)
                .WithMany(u => u.FamiliaUsuarios)
                .HasForeignKey(fu => fu.UsuarioId)
                .IsRequired();
        });

        modelBuilder.Entity<TecnicoCras>(entity =>
        {
            // Composite primary key
            entity.HasKey(tc => new { tc.CrasId, tc.TecnicoId });

            // Relationship with Cras
            entity.HasOne(tc => tc.Cras)
                .WithMany(c => c.TecnicosCras)
                .HasForeignKey(tc => tc.CrasId)
                .IsRequired();

            // Relationship with Tecnico
            entity.HasOne(tc => tc.Tecnico)
                .WithMany(t => t.TecnicoCras)
                .HasForeignKey(tc => tc.TecnicoId)
                .IsRequired();
        });
    }

    private static string GetSQLiteConnection()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "siscras.db");
        return dbPath;
    }
}