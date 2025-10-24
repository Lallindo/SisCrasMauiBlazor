using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public abstract class BaseRepositoriesTests : IAsyncLifetime
    {
        private SqliteConnection _connection;
        protected SisCrasDbContext _context;

        public virtual async Task InitializeAsync()
        {
            // Create and open in-memory database connection
            _connection = new SqliteConnection("Filename=:memory:");
            await _connection.OpenAsync();

            var options = new DbContextOptionsBuilder<SisCrasDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new SisCrasDbContext(options);
            
            // Create the database schema
            await _context.Database.EnsureCreatedAsync();
        }

        public virtual async Task DisposeAsync()
        {
            await _context.DisposeAsync();
            await _connection.DisposeAsync();
        }

        protected async Task ClearDatabaseAsync()
        {
            try
            {
                // Use a fresh context to avoid tracking issues
                var options = new DbContextOptionsBuilder<SisCrasDbContext>()
                    .UseSqlite(_connection)
                    .Options;

                using var freshContext = new SisCrasDbContext(options);
                
                // Clear in correct order to respect foreign key constraints
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM Prontuarios;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM FamiliaUsuarios;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM TecnicoCras;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM Familias;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM Usuarios;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM Tecnicos;");
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM Cras;");
                
                // Reset SQLite sequences
                await freshContext.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name IN ('Prontuarios', 'FamiliaUsuarios', 'TecnicoCras', 'Familias', 'Usuarios', 'Tecnicos', 'Cras');");
            }
            catch (Exception ex)
            {
                // If raw SQL fails, try the entity approach but handle exceptions
                try
                {
                    _context.ChangeTracker.Clear();
                    
                    // Only remove entities that exist
                    if (await _context.Prontuarios.AnyAsync())
                        _context.Prontuarios.RemoveRange(_context.Prontuarios);
                    
                    if (await _context.FamiliaUsuarios.AnyAsync())
                        _context.FamiliaUsuarios.RemoveRange(_context.FamiliaUsuarios);
                    
                    if (await _context.TecnicoCras.AnyAsync())
                        _context.TecnicoCras.RemoveRange(_context.TecnicoCras);
                    
                    if (await _context.Familias.AnyAsync())
                        _context.Familias.RemoveRange(_context.Familias);
                    
                    if (await _context.Usuarios.AnyAsync())
                        _context.Usuarios.RemoveRange(_context.Usuarios);
                    
                    if (await _context.Tecnicos.AnyAsync())
                        _context.Tecnicos.RemoveRange(_context.Tecnicos);
                    
                    if (await _context.Cras.AnyAsync())
                        _context.Cras.RemoveRange(_context.Cras);
                    
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    // If all else fails, recreate the database
                    await _context.Database.EnsureDeletedAsync();
                    await _context.Database.EnsureCreatedAsync();
                }
            }
        }
    }
}