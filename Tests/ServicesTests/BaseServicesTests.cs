using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public abstract class ServiceTestBase : IAsyncLifetime
    {
        private SqliteConnection _connection;
        protected SisCrasDbContext _context;

        public virtual async Task InitializeAsync()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            await _connection.OpenAsync();

            var options = new DbContextOptionsBuilder<SisCrasDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new SisCrasDbContext(options);
            await _context.Database.EnsureCreatedAsync();
        }

        public virtual async Task DisposeAsync()
        {
            await _context.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}