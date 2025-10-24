using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SisCras.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SisCrasDbContext>
{
    public SisCrasDbContext CreateDbContext(string[] args)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "siscras.db");

        var optionsBuilder = new DbContextOptionsBuilder<SisCrasDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new SisCrasDbContext(optionsBuilder.Options);
    }
}