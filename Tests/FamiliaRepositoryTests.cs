using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using Xunit;

namespace SisCras.Tests;

public class FamiliaRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly SisCrasDbContext _context;
    private readonly FamiliaRepository _repository;

    public FamiliaRepositoryTests()
    {
        // Configura a conexão SQLite em memória
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<SisCrasDbContext>()
            .UseSqlite(_connection)
            .Options;

        // Cria o DbContext e o esquema
        _context = new SisCrasDbContext(options);
        _context.Database.EnsureCreated();

        // Instancia o repositório real
        _repository = new FamiliaRepository(_context);
    }

    [Fact]
    public async Task GetResponsavelFromFamilia_DeveRetornarResponsavelAtivo()
    {

        // Melhorar a automação desses testes
        
        int idDaFamiliaParaBuscar = 1;

        int idEsperadoDoResponsavel = 1; 
        string nomeEsperadoDoResponsavel = "bruno"; 

        var resultado = await _repository.GetResponsavelFromFamilia(idDaFamiliaParaBuscar);
        
        Assert.NotNull(resultado); 
        Assert.Equal(idEsperadoDoResponsavel, resultado.Id);
        Assert.Equal(nomeEsperadoDoResponsavel, resultado.Nome); 
    }

    // Garante que a conexão seja fechada e o BD em memória descartado
    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}