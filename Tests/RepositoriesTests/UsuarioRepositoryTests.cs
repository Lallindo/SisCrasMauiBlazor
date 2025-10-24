using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SisCras.Database;
using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using Xunit;

namespace SisCras.Tests.RepositoriesTests;

public class UsuarioRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly SisCrasDbContext _context;
    private readonly UsuarioRepository _repository;
    
    // Test data constants
    private const string CpfUsuario1 = "111.111.111-11";
    private const string CpfUsuario2 = "222.222.222-22";
    private const string CpfUsuario3 = "333.333.333-33";
    private const string CpfUsuario4 = "444.444.444-44";
    private const string CpfInexistente = "999.999.999-99";
    
    private const string NomeUsuario1 = "Usuario Um";
    private const string NomeUsuario2 = "Usuario Dois";
    private const string NomeUsuario3 = "Usuario Com Familias";
    private const string NomeUsuario4 = "Usuario Solitario";
    private const string NomeInexistente = "Usuario Inexistente";

    public UsuarioRepositoryTests()
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

        // Instancia os repositórios reais
        _repository = new UsuarioRepository(_context);
    }

    // Helpers para criar o model exigido
    private Usuario CriarUsuarioValido(string nome, string cpf, string nis, string rg, DateOnly dataNascimento)
    {
        return new Usuario
        {
            Nome = nome,
            Cpf = cpf,
            DataNascimento = dataNascimento,
            Escolaridade = EscolaridadeEnum.MedCompleto,
            EstadoCivil = EstadoCivilEnum.Solteiro,
            FonteRenda = FonteRendaEnum.TrabalhoInformal,
            Nis = nis,
            Ocupacao = "Ocupação Teste",
            OrientacaoSexual = OrientacaoSexualEnum.Heterossexual,
            Profissao = "Profissão Teste",
            Raca = RacaEnum.Branca,
            RendaBruta = 1000f,
            Rg = rg,
            Sexo = SexoEnum.Masculino,
        };
    }
    
    private async Task<Usuario> CriarESalvarUsuarioUnico()
    {
        var usuario = CriarUsuarioValido(NomeUsuario1, CpfUsuario1, "111", "111", new DateOnly(1990, 1, 1));
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    private async Task<(Usuario usuario1, Usuario usuario2)> CriarESalvarDoisUsuarios()
    {
        var usuario1 = CriarUsuarioValido(NomeUsuario1, CpfUsuario1, "111", "111", new DateOnly(1990, 1, 1));
        var usuario2 = CriarUsuarioValido(NomeUsuario2, CpfUsuario2, "222", "222", new DateOnly(1991, 2, 2));
        
        _context.Usuarios.AddRange(usuario1, usuario2);
        await _context.SaveChangesAsync();
        
        return (usuario1, usuario2);
    }

    // Helpers para "Assert"
    private void AssertUsuarioEquals(Usuario expected, Usuario actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Nome, actual.Nome);
        Assert.Equal(expected.Cpf, actual.Cpf);
    }

    private void AssertUsuarioBasico(Usuario usuario, string nomeEsperado, string cpfEsperado)
    {
        Assert.NotNull(usuario);
        Assert.Equal(nomeEsperado, usuario.Nome);
        Assert.Equal(cpfEsperado, usuario.Cpf);
    }

    [Fact]
    public async Task GetByCpf_DeveRetornarUsuarioCorreto()
    {
        // Arrange
        var (usuario1, usuario2) = await CriarESalvarDoisUsuarios();

        // Act
        var resultado = await _repository.GetByCpf(CpfUsuario1);

        // Assert
        AssertUsuarioEquals(usuario1, resultado);
    }

    [Fact]
    public async Task GetByCpf_DeveRetornarNullSeNaoEncontrarCpf()
    {
        // Arrange - no users created

        // Act
        var resultado = await _repository.GetByCpf(CpfInexistente);

        // Assert
        Assert.Null(resultado);
    }
    
    [Fact]
    public async Task GetByNome_DeveRetornarUsuarioCorreto()
    {
        // Arrange
        await CriarESalvarUsuarioUnico();

        // Act
        var resultado = await _repository.GetByNome(NomeUsuario1);

        // Assert
        AssertUsuarioBasico(resultado, NomeUsuario1, CpfUsuario1);
    }
    
    [Fact]
    public async Task GetByNome_DeveRetornarNullSeNaoEncontrarNome()
    {
        // Arrange
        await CriarESalvarUsuarioUnico();

        // Act
        var resultado = await _repository.GetByNome(NomeInexistente);

        // Assert
        Assert.Null(resultado);
    }
    
    [Fact]
    public async Task GetByDataNascimento_DeveRetornarUsuarioCorreto()
    {
        // Arrange
        var (usuario1, usuario2) = await CriarESalvarDoisUsuarios();
        var dataParaBuscar = new DateOnly(1990, 1, 1);

        // Act
        var resultado = await _repository.GetByDataNascimento(dataParaBuscar);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(usuario1.Id, resultado.Id);
        Assert.Equal(NomeUsuario1, resultado.Nome);
        Assert.Equal(dataParaBuscar, resultado.DataNascimento);
    }
    
    [Fact]
    public async Task GetByDataNascimento_DeveRetornarNullSeNaoEncontrarData()
    {
        // Arrange
        await CriarESalvarUsuarioUnico();
        var dataInexistente = new DateOnly(2000, 1, 1);

        // Act
        var resultado = await _repository.GetByDataNascimento(dataInexistente);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task GetFamiliasFromUsuario_DeveRetornarFamiliasCorretas()
    {
        // Arrange
        var usuario1 = CriarUsuarioValido(NomeUsuario3, CpfUsuario3, "333", "333", new DateOnly(1992, 3, 3));
        var familia1 = new Familia();
        var familia2 = new Familia();
        var familiaSemRelacao = new Familia();

        _context.Usuarios.Add(usuario1);
        _context.Familias.AddRange(familia1, familia2, familiaSemRelacao);
        await _context.SaveChangesAsync();

        // Adiciona relações entre usuario1 e familia1/familia2
        var relacao1 = new FamiliaUsuario { Familia = familia1, Usuario = usuario1, Parentesco = ParentescoEnum.Responsavel, Ativo = true };
        var relacao2 = new FamiliaUsuario { Familia = familia2, Usuario = usuario1, Parentesco = ParentescoEnum.Filho, Ativo = true };
        _context.FamiliaUsuarios.AddRange(relacao1, relacao2);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.GetFamiliasFromUsuario(usuario1.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        Assert.Contains(resultado, f => f.Id == familia1.Id);
        Assert.Contains(resultado, f => f.Id == familia2.Id);
        Assert.DoesNotContain(resultado, f => f.Id == familiaSemRelacao.Id);
    }
     
    [Fact]
    public async Task GetFamiliasFromUsuario_DeveRetornarListaVaziaSeUsuarioNaoTemFamilias()
    {
        // Arrange
        var usuarioSemFamilia = CriarUsuarioValido(NomeUsuario4, CpfUsuario4, "444", "444", new DateOnly(1993, 4, 4));
        _context.Usuarios.Add(usuarioSemFamilia);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.GetFamiliasFromUsuario(usuarioSemFamilia.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }
    
    // Garante que a conexão seja fechada e o BD em memória descartado
    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}