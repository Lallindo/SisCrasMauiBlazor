using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using Xunit;

namespace SisCras.Tests.RepositoriesTests;

public class CrasRepositoryTests : BaseRepositoriesTests
{
    private CrasRepository _repository;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _repository = new CrasRepository(_context);
        await ClearDatabaseAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddCras()
    {
        // Arrange
        var cras = ModelFactory.CreateCras("CRAS Centro");

        // Act
        var result = await _repository.AddAsync(cras);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("CRAS Centro", result.Nome);
        Assert.True(result.Id > 0);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCras()
    {
        // Arrange
        var cras = ModelFactory.CreateCras("CRAS Centro");
        await _repository.AddAsync(cras);

        // Act
        var result = await _repository.GetByIdAsync(cras.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cras.Id, result.Id);
        Assert.Equal("CRAS Centro", result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullForNonExistentCras()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCras()
    {
        // Arrange
        await _repository.AddAsync(ModelFactory.CreateCras("CRAS Centro"));
        await _repository.AddAsync(ModelFactory.CreateCras("CRAS Norte"));

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListWhenNoCras()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCras()
    {
        // Arrange
        var cras = ModelFactory.CreateCras("CRAS Original");
        await _repository.AddAsync(cras);

        // Act
        cras.Nome = "CRAS Atualizado";
        await _repository.UpdateAsync(cras);

        // Assert
        var updatedCras = await _repository.GetByIdAsync(cras.Id);
        Assert.NotNull(updatedCras);
        Assert.Equal("CRAS Atualizado", updatedCras.Nome);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCras()
    {
        // Arrange
        var cras = ModelFactory.CreateCras("CRAS Para Deletar");
        await _repository.AddAsync(cras);
        var initialCount = (await _repository.GetAllAsync()).Count;

        // Act
        await _repository.DeleteAsync(cras);

        // Assert
        var finalCount = (await _repository.GetAllAsync()).Count;
        var deletedCras = await _repository.GetByIdAsync(cras.Id);

        Assert.Null(deletedCras);
        Assert.Equal(initialCount - 1, finalCount);
    }

    [Fact]
    public async Task GetTecnicosFromCras_ShouldReturnTecnicos()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var tecnico = ModelFactory.CreateTecnico("João", "joao");
        _context.Tecnicos.Add(tecnico);
        await _context.SaveChangesAsync();

        var tecnicoCras = ModelFactory.CreateTecnicoCras(tecnico.Id, cras.Id);
        _context.TecnicoCras.Add(tecnicoCras);
        await _context.SaveChangesAsync();

        // Act
        var tecnicos = await _repository.GetTecnicosFromCras(cras.Id);

        // Assert
        Assert.NotNull(tecnicos);
        Assert.Single(tecnicos);
        Assert.Equal("João", tecnicos.First().Nome);
    }

    [Fact]
    public async Task GetTecnicosFromCras_WithCrasObject_ShouldReturnTecnicos()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var tecnico = ModelFactory.CreateTecnico("Maria", "maria");
        _context.Tecnicos.Add(tecnico);
        await _context.SaveChangesAsync();

        var tecnicoCras = ModelFactory.CreateTecnicoCras(tecnico.Id, cras.Id);
        _context.TecnicoCras.Add(tecnicoCras);
        await _context.SaveChangesAsync();

        // Act
        var tecnicos = await _repository.GetTecnicosFromCras(cras);

        // Assert
        Assert.NotNull(tecnicos);
        Assert.Single(tecnicos);
        Assert.Equal("Maria", tecnicos.First().Nome);
    }

    [Fact]
    public async Task GetTecnicosFromCras_ShouldReturnEmptyWhenNoTecnicos()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        // Act
        var tecnicos = await _repository.GetTecnicosFromCras(cras.Id);

        // Assert
        Assert.NotNull(tecnicos);
        Assert.Empty(tecnicos);
    }

    [Fact]
    public async Task GetProntuariosFromCras_ShouldReturnProntuarios()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var familia = ModelFactory.CreateFamilia();
        _context.Familias.Add(familia);

        var tecnico = ModelFactory.CreateTecnico();
        _context.Tecnicos.Add(tecnico);

        await _context.SaveChangesAsync();

        var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 123);
        _context.Prontuarios.Add(prontuario);
        await _context.SaveChangesAsync();

        // Act
        var prontuarios = await _repository.GetProntuariosFromCras(cras.Id);

        // Assert
        Assert.NotNull(prontuarios);
        Assert.Single(prontuarios);
        Assert.Equal(123, prontuarios.First().Codigo);
    }

    [Fact]
    public async Task GetProntuariosFromCras_WithCrasObject_ShouldReturnProntuarios()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var familia = ModelFactory.CreateFamilia();
        _context.Familias.Add(familia);

        var tecnico = ModelFactory.CreateTecnico();
        _context.Tecnicos.Add(tecnico);

        await _context.SaveChangesAsync();

        var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 456);
        _context.Prontuarios.Add(prontuario);
        await _context.SaveChangesAsync();

        // Act
        var prontuarios = await _repository.GetProntuariosFromCras(cras);

        // Assert
        Assert.NotNull(prontuarios);
        Assert.Single(prontuarios);
        Assert.Equal(456, prontuarios.First().Codigo);
    }

    [Fact]
    public async Task GetFamiliasFromCras_ShouldReturnFamilias()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var familia = ModelFactory.CreateFamilia();
        _context.Familias.Add(familia);

        var tecnico = ModelFactory.CreateTecnico();
        _context.Tecnicos.Add(tecnico);

        await _context.SaveChangesAsync();

        var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id);
        _context.Prontuarios.Add(prontuario);
        await _context.SaveChangesAsync();

        // Act
        var familias = await _repository.GetFamiliasFromCras(cras.Id);

        // Assert
        Assert.NotNull(familias);
        Assert.Single(familias);
        Assert.Equal(familia.Id, familias.First().Id);
    }

    [Fact]
    public async Task GetFamiliasFromCras_WithCrasObject_ShouldReturnFamilias()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var familia = ModelFactory.CreateFamilia();
        _context.Familias.Add(familia);

        var tecnico = ModelFactory.CreateTecnico();
        _context.Tecnicos.Add(tecnico);

        await _context.SaveChangesAsync();

        var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id);
        _context.Prontuarios.Add(prontuario);
        await _context.SaveChangesAsync();

        // Act
        var familias = await _repository.GetFamiliasFromCras(cras);

        // Assert
        Assert.NotNull(familias);
        Assert.Single(familias);
        Assert.Equal(familia.Id, familias.First().Id);
    }

    [Fact]
    public async Task GetProntuarioAndFamiliaAndUsuariosFromCras_ShouldReturnProntuariosWithFamiliesAndUsers()
    {
        // Arrange
        var cras = ModelFactory.CreateCras();
        await _repository.AddAsync(cras);

        var familia = ModelFactory.CreateFamilia();
        _context.Familias.Add(familia);

        var tecnico = ModelFactory.CreateTecnico();
        _context.Tecnicos.Add(tecnico);

        var usuarios = new List<Usuario>
        {
            ModelFactory.CreateUsuario("Responsavel Familia", "111.111.111-11", "11111111111"),
            ModelFactory.CreateUsuario("Filho 1", "222.222.222-22", "22222222222"),
            ModelFactory.CreateUsuario("Filho 2", "333.333.333-33", "33333333333")
        };
        _context.Usuarios.AddRange(usuarios);

        // Save all entities first to get their IDs
        await _context.SaveChangesAsync();

        // Create FamiliaUsuario relationships AFTER saving usuarios and familia
        var familiaUsuarios = new List<FamiliaUsuario>
        {
            ModelFactory.CreateFamiliaUsuario(familia.Id, usuarios[0].Id, ParentescoEnum.Responsavel, true),
            ModelFactory.CreateFamiliaUsuario(familia.Id, usuarios[1].Id, ParentescoEnum.Filho, true),
            ModelFactory.CreateFamiliaUsuario(familia.Id, usuarios[2].Id, ParentescoEnum.Filho, true)
        };
        _context.FamiliaUsuarios.AddRange(familiaUsuarios);

        // Create prontuario AFTER saving all related entities
        var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001);
        _context.Prontuarios.Add(prontuario);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetProntuarioAndFamiliaAndUsuariosFromCras(cras.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var prontuarioResult = result.First();
        Assert.NotNull(prontuarioResult.Familia);
        Assert.Equal(3, prontuarioResult.Familia.FamiliaUsuarios.Count);

        // Verify that users are loaded
        Assert.All(prontuarioResult.Familia.FamiliaUsuarios, fu => Assert.NotNull(fu.Usuario));
        Assert.Contains(prontuarioResult.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Responsavel Familia");
        Assert.Contains(prontuarioResult.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Filho 1");
        Assert.Contains(prontuarioResult.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Filho 2");
    }

    [Fact]
    public async Task GetProntuarioAndFamiliaAndUsuariosFromCras_ShouldReturnOnlyProntuariosFromSpecifiedCras()
    {
        // Arrange
        // Create and save first CRAS
        var cras1 = ModelFactory.CreateCras("CRAS 1");
        await _repository.AddAsync(cras1);

        // Create and save entities for first CRAS
        var familia1 = ModelFactory.CreateFamilia();
        var tecnico1 = ModelFactory.CreateTecnico();
        var usuarios1 = new List<Usuario>
        {
            ModelFactory.CreateUsuario("Usuario 1-1", "111.111.111-11", "11111111111"),
            ModelFactory.CreateUsuario("Usuario 1-2", "222.222.222-22", "22222222222")
        };

        _context.Familias.Add(familia1);
        _context.Tecnicos.Add(tecnico1);
        _context.Usuarios.AddRange(usuarios1);
        await _context.SaveChangesAsync(); // Save to get IDs

        // Create relationships for first CRAS
        var familiaUsuarios1 = new List<FamiliaUsuario>
        {
            ModelFactory.CreateFamiliaUsuario(familia1.Id, usuarios1[0].Id, ParentescoEnum.Responsavel, true),
            ModelFactory.CreateFamiliaUsuario(familia1.Id, usuarios1[1].Id, ParentescoEnum.Filho, true)
        };
        _context.FamiliaUsuarios.AddRange(familiaUsuarios1);

        var prontuario1 = ModelFactory.CreateProntuario(cras1.Id, familia1.Id, tecnico1.Id, 1001);
        _context.Prontuarios.Add(prontuario1);

        // Create and save second CRAS
        var cras2 = ModelFactory.CreateCras("CRAS 2");
        await _repository.AddAsync(cras2);

        // Create and save entities for second CRAS
        var familia2 = ModelFactory.CreateFamilia();
        var tecnico2 = ModelFactory.CreateTecnico();
        var usuarios2 = new List<Usuario>
        {
            ModelFactory.CreateUsuario("Usuario 2-1", "333.333.333-33", "33333333333"),
            ModelFactory.CreateUsuario("Usuario 2-2", "444.444.444-44", "44444444444")
        };

        _context.Familias.Add(familia2);
        _context.Tecnicos.Add(tecnico2);
        _context.Usuarios.AddRange(usuarios2);
        await _context.SaveChangesAsync(); // Save to get IDs

        // Create relationships for second CRAS
        var familiaUsuarios2 = new List<FamiliaUsuario>
        {
            ModelFactory.CreateFamiliaUsuario(familia2.Id, usuarios2[0].Id, ParentescoEnum.Responsavel, true),
            ModelFactory.CreateFamiliaUsuario(familia2.Id, usuarios2[1].Id, ParentescoEnum.Filho, true)
        };
        _context.FamiliaUsuarios.AddRange(familiaUsuarios2);

        var prontuario2 = ModelFactory.CreateProntuario(cras2.Id, familia2.Id, tecnico2.Id, 2001);
        _context.Prontuarios.Add(prontuario2);

        // Save all relationships and prontuarios
        await _context.SaveChangesAsync();

        // Act
        var resultCras1 = await _repository.GetProntuarioAndFamiliaAndUsuariosFromCras(cras1.Id);
        var resultCras2 = await _repository.GetProntuarioAndFamiliaAndUsuariosFromCras(cras2.Id);

        // Assert
        Assert.NotNull(resultCras1);
        Assert.Single(resultCras1);
        Assert.All(resultCras1, p => Assert.Equal(cras1.Id, p.CrasId));
        Assert.Equal(1001, resultCras1.First().Codigo);

        Assert.NotNull(resultCras2);
        Assert.Single(resultCras2);
        Assert.All(resultCras2, p => Assert.Equal(cras2.Id, p.CrasId));
        Assert.Equal(2001, resultCras2.First().Codigo);
    }
}