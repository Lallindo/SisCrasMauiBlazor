using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using Xunit;

namespace SisCras.Tests.RepositoriesTests;

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

    // --- Helper para criar um usuário com dados obrigatórios ---
    private Usuario CriarUsuarioValido(string nome, string cpf, string nis, string rg, DateOnly dataNascimento, bool ativo = true)
    {
        return new Usuario
        {
            Nome = nome,
            Cpf = cpf,
            DataNascimento = dataNascimento,
            Escolaridade = EscolaridadeEnum.MedCompleto, // Valor padrão
            EstadoCivil = EstadoCivilEnum.Solteiro, // Valor padrão
            FonteRenda = FonteRendaEnum.TrabalhoInformal, // Valor padrão
            Nis = nis,
            Ocupacao = "Ocupação Teste", // Valor padrão
            OrientacaoSexual = OrientacaoSexualEnum.Heterossexual, // Valor padrão
            Profissao = "Profissão Teste", // Valor padrão
            Raca = RacaEnum.Branca, // Valor padrão
            RendaBruta = 1000f, // Valor padrão
            Rg = rg,
            Sexo = SexoEnum.Masculino, // Valor padrão
            // FamiliaUsuarios será gerenciado pela relação
        };
    }

    [Fact]
    public async Task GetResponsavelFromFamilia_DeveRetornarResponsavelAtivo()
    {
        // =========== ARRANGE (Organizar) ===========
        var familia1 = new Familia();
        var usuarioResponsavel = CriarUsuarioValido("Responsável Ativo", "111", "111", "111", new DateOnly(1980, 1, 1));
        var usuarioConjuge = CriarUsuarioValido("Cônjuge Ativo", "222", "222", "222", new DateOnly(1982, 2, 2));

        _context.Usuarios.AddRange(usuarioResponsavel, usuarioConjuge);
        _context.Familias.Add(familia1);
        await _context.SaveChangesAsync();

        var relacaoResponsavel = new FamiliaUsuario { Familia = familia1, Usuario = usuarioResponsavel, Parentesco = ParentescoEnum.Responsavel, Ativo = true };
        var relacaoConjuge = new FamiliaUsuario { Familia = familia1, Usuario = usuarioConjuge, Parentesco = ParentescoEnum.Conjuge, Ativo = true };
        _context.FamiliaUsuarios.AddRange(relacaoResponsavel, relacaoConjuge);
        await _context.SaveChangesAsync();

        // =========== ACT (Agir) ===========
        var resultado = await _repository.GetResponsavelFromFamilia(familia1.Id);

        // =========== ASSERT (Verificar) ===========
        Assert.NotNull(resultado);
        Assert.Equal(usuarioResponsavel.Id, resultado.Id);
        Assert.Equal("Responsável Ativo", resultado.Nome);
    }

    [Fact]
    public async Task GetResponsavelFromFamilia_DeveRetornarNullSeResponsavelInativo()
    {
        // =========== ARRANGE (Organizar) ===========
        var familia1 = new Familia();
        var usuarioResponsavelInativo = CriarUsuarioValido("Responsável Inativo", "333", "333", "333", new DateOnly(1985, 3, 3));

        _context.Usuarios.Add(usuarioResponsavelInativo);
        _context.Familias.Add(familia1);
        await _context.SaveChangesAsync();

        // Relação com Ativo = false
        var relacaoResponsavel = new FamiliaUsuario { Familia = familia1, Usuario = usuarioResponsavelInativo, Parentesco = ParentescoEnum.Responsavel, Ativo = false };
        _context.FamiliaUsuarios.Add(relacaoResponsavel);
        await _context.SaveChangesAsync();

        // =========== ACT (Agir) ===========
        var resultado = await _repository.GetResponsavelFromFamilia(familia1.Id);

        // =========== ASSERT (Verificar) ===========
        Assert.Null(resultado); // Espera-se null porque o único responsável está inativo na família
    }

     [Fact]
    public async Task GetActiveUsuariosFromFamilia_DeveRetornarApenasUsuariosAtivos()
    {
        // =========== ARRANGE (Organizar) ===========
        var familia1 = new Familia();
        var usuarioAtivo1 = CriarUsuarioValido("Ativo Um", "444", "444", "444", new DateOnly(1990, 4, 4));
        var usuarioAtivo2 = CriarUsuarioValido("Ativo Dois", "555", "555", "555", new DateOnly(1991, 5, 5));
        var usuarioInativo = CriarUsuarioValido("Inativo", "666", "666", "666", new DateOnly(1992, 6, 6));

        _context.Usuarios.AddRange(usuarioAtivo1, usuarioAtivo2, usuarioInativo);
        _context.Familias.Add(familia1);
        await _context.SaveChangesAsync();

        var relacaoAtivo1 = new FamiliaUsuario { Familia = familia1, Usuario = usuarioAtivo1, Parentesco = ParentescoEnum.Responsavel, Ativo = true };
        var relacaoAtivo2 = new FamiliaUsuario { Familia = familia1, Usuario = usuarioAtivo2, Parentesco = ParentescoEnum.Filho, Ativo = true };
        var relacaoInativo = new FamiliaUsuario { Familia = familia1, Usuario = usuarioInativo, Parentesco = ParentescoEnum.Filho, Ativo = false }; // Inativo na família
        _context.FamiliaUsuarios.AddRange(relacaoAtivo1, relacaoAtivo2, relacaoInativo);
        await _context.SaveChangesAsync();

        // =========== ACT (Agir) ===========
        var resultado = await _repository.GetActiveUsuariosFromFamilia(familia1.Id);

        // =========== ASSERT (Verificar) ===========
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count); // Deve retornar apenas os 2 ativos
        Assert.Contains(resultado, u => u.Nome == "Ativo Um");
        Assert.Contains(resultado, u => u.Nome == "Ativo Dois");
        Assert.DoesNotContain(resultado, u => u.Nome == "Inativo"); // Não deve conter o inativo
    }

    [Fact]
    public async Task GetUsuariosFromFamilia_DeveRetornarTodosUsuariosMesmoInativos()
    {
        // =========== ARRANGE (Organizar) ===========
        var familia1 = new Familia();
        var usuarioAtivo = CriarUsuarioValido("Ativo", "777", "777", "777", new DateOnly(1995, 7, 7));
        var usuarioInativo = CriarUsuarioValido("Inativo na Familia", "888", "888", "888", new DateOnly(1996, 8, 8));

        _context.Usuarios.AddRange(usuarioAtivo, usuarioInativo);
        _context.Familias.Add(familia1);
        await _context.SaveChangesAsync();

        var relacaoAtivo = new FamiliaUsuario { Familia = familia1, Usuario = usuarioAtivo, Parentesco = ParentescoEnum.Responsavel, Ativo = true };
        var relacaoInativo = new FamiliaUsuario { Familia = familia1, Usuario = usuarioInativo, Parentesco = ParentescoEnum.Filho, Ativo = false }; // Inativo na família
        _context.FamiliaUsuarios.AddRange(relacaoAtivo, relacaoInativo);
        await _context.SaveChangesAsync();

        // =========== ACT (Agir) ===========
        var resultado = await _repository.GetUsuariosFromFamilia(familia1.Id);

        // =========== ASSERT (Verificar) ===========
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count); // Deve retornar ambos, ativo e inativo
        Assert.Contains(resultado, u => u.Nome == "Ativo");
        Assert.Contains(resultado, u => u.Nome == "Inativo na Familia");
    }

    // Garante que a conexão seja fechada e o BD em memória descartado
    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}