using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public class UsuarioRepository : BaseRepositoriesTests
    {
        private Repositories.UsuarioRepository _repository;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _repository = new Repositories.UsuarioRepository(_context);
            await ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddUsuario()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("João Silva", "12345678901", "12345678901");

            // Act
            var result = await _repository.AddAsync(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("João Silva", result.Nome);
            Assert.Equal("12345678901", result.Cpf);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUsuario()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Maria", "12345678902", "12345678902");
            await _repository.AddAsync(usuario);

            // Act
            var result = await _repository.GetByIdAsync(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.Id, result.Id);
            Assert.Equal("Maria", result.Nome);
            Assert.Equal("12345678902", result.Cpf);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullForNonExistentUsuario()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsuarios()
        {
            // Arrange
            await _repository.AddAsync(ModelFactory.CreateUsuario("Usuario 1", "11111111111", "11111111111"));
            await _repository.AddAsync(ModelFactory.CreateUsuario("Usuario 2", "22222222222", "22222222222"));
            await _repository.AddAsync(ModelFactory.CreateUsuario("Usuario 3", "33333333333", "33333333333"));

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyListWhenNoUsuarios()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUsuario()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Original", "11111111111", "11111111111");
            await _repository.AddAsync(usuario);

            // Act
            usuario.Nome = "Usuario Atualizado";
            usuario.Cpf = "99999999999";
            await _repository.UpdateAsync(usuario);

            // Assert
            var updatedUsuario = await _repository.GetByIdAsync(usuario.Id);
            Assert.NotNull(updatedUsuario);
            Assert.Equal("Usuario Atualizado", updatedUsuario.Nome);
            Assert.Equal("99999999999", updatedUsuario.Cpf);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUsuario()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Para Deletar", "11111111111", "11111111111");
            await _repository.AddAsync(usuario);
            var initialCount = (await _repository.GetAllAsync()).Count;

            // Act
            await _repository.DeleteAsync(usuario);

            // Assert
            var finalCount = (await _repository.GetAllAsync()).Count;
            var deletedUsuario = await _repository.GetByIdAsync(usuario.Id);
            
            Assert.Null(deletedUsuario);
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_ShouldReturnActiveFamilia()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Maria", "12345678902", "12345678902");
            await _repository.AddAsync(usuario);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Filho, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetActiveFamiliaFromUsuario(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familia.Id, result.Id);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_ShouldReturnNullWhenNoActiveFamilia()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Sem Familia", "11111111111", "11111111111");
            await _repository.AddAsync(usuario);

            // Act
            var result = await _repository.GetActiveFamiliaFromUsuario(usuario.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_WithUsuarioObject_ShouldReturnActiveFamilia()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Carlos", "12345678903", "12345678903");
            await _repository.AddAsync(usuario);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Responsavel, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetActiveFamiliaFromUsuario(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familia.Id, result.Id);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_ShouldReturnAllFamilias()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Carlos", "12345678903", "12345678903");
            await _repository.AddAsync(usuario);

            var familia1 = ModelFactory.CreateFamilia();
            var familia2 = ModelFactory.CreateFamilia();
            _context.Familias.AddRange(familia1, familia2);
            await _context.SaveChangesAsync();

            var familiaUsuario1 = ModelFactory.CreateFamiliaUsuario(familia1.Id, usuario.Id, ParentescoEnum.Filho, true);
            var familiaUsuario2 = ModelFactory.CreateFamiliaUsuario(familia2.Id, usuario.Id, ParentescoEnum.Conjuge, false);
            _context.FamiliaUsuarios.AddRange(familiaUsuario1, familiaUsuario2);
            await _context.SaveChangesAsync();

            // Act
            var familias = await _repository.GetFamiliasFromUsuario(usuario.Id);

            // Assert
            Assert.NotNull(familias);
            Assert.Equal(2, familias.Count);
            Assert.Contains(familias, f => f.Id == familia1.Id);
            Assert.Contains(familias, f => f.Id == familia2.Id);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_ShouldReturnEmptyListWhenNoFamilias()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Sem Familias", "11111111111", "11111111111");
            await _repository.AddAsync(usuario);

            // Act
            var familias = await _repository.GetFamiliasFromUsuario(usuario.Id);

            // Assert
            Assert.NotNull(familias);
            Assert.Empty(familias);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_WithUsuarioObject_ShouldReturnAllFamilias()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Ana", "12345678904", "12345678904");
            await _repository.AddAsync(usuario);

            var familia1 = ModelFactory.CreateFamilia();
            var familia2 = ModelFactory.CreateFamilia();
            _context.Familias.AddRange(familia1, familia2);
            await _context.SaveChangesAsync();

            var familiaUsuario1 = ModelFactory.CreateFamiliaUsuario(familia1.Id, usuario.Id, ParentescoEnum.Filho, true);
            var familiaUsuario2 = ModelFactory.CreateFamiliaUsuario(familia2.Id, usuario.Id, ParentescoEnum.Conjuge, true);
            _context.FamiliaUsuarios.AddRange(familiaUsuario1, familiaUsuario2);
            await _context.SaveChangesAsync();

            // Act
            var familias = await _repository.GetFamiliasFromUsuario(usuario);

            // Assert
            Assert.NotNull(familias);
            Assert.Equal(2, familias.Count);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_ShouldIncludeProntuarios()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Com Prontuarios", "11111111111", "11111111111");
            await _repository.AddAsync(usuario);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);
            
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);
            
            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);
            
            await _context.SaveChangesAsync();

            // Add prontuario to familia
            var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001);
            _context.Prontuarios.Add(prontuario);
            
            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Responsavel, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            
            await _context.SaveChangesAsync();

            // Act
            var familias = await _repository.GetFamiliasFromUsuario(usuario.Id);

            // Assert
            Assert.NotNull(familias);
            Assert.Single(familias);
            var familiaResult = familias.First();
            Assert.NotNull(familiaResult.Prontuarios);
            Assert.Single(familiaResult.Prontuarios);
            Assert.Equal(1001, familiaResult.Prontuarios.First().Codigo);
        }

        [Fact]
        public async Task ComplexScenario_UsuarioWithMultipleActiveAndInactiveFamilias()
        {
            // Arrange
            var usuario = ModelFactory.CreateUsuario("Usuario Complexo", "12345678999", "12345678999");
            await _repository.AddAsync(usuario);

            var familiaAtiva1 = ModelFactory.CreateFamilia();
            var familiaAtiva2 = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaUnipessoalFem);
            var familiaInativa = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaExtensa);
            
            _context.Familias.AddRange(familiaAtiva1, familiaAtiva2, familiaInativa);
            await _context.SaveChangesAsync();

            // Active familia associations
            var familiaUsuarioAtiva1 = ModelFactory.CreateFamiliaUsuario(familiaAtiva1.Id, usuario.Id, ParentescoEnum.Responsavel, true);
            var familiaUsuarioAtiva2 = ModelFactory.CreateFamiliaUsuario(familiaAtiva2.Id, usuario.Id, ParentescoEnum.Filho, true);
            // Inactive familia association
            var familiaUsuarioInativa = ModelFactory.CreateFamiliaUsuario(familiaInativa.Id, usuario.Id, ParentescoEnum.Conjuge, false);
            
            _context.FamiliaUsuarios.AddRange(familiaUsuarioAtiva1, familiaUsuarioAtiva2, familiaUsuarioInativa);
            await _context.SaveChangesAsync();

            // Act - Get active familia
            var activeFamilia = await _repository.GetActiveFamiliaFromUsuario(usuario.Id);

            // Act - Get all familias
            var allFamilias = await _repository.GetFamiliasFromUsuario(usuario.Id);

            // Assert
            Assert.NotNull(activeFamilia);
            // Should return the first active familia found (implementation detail)
            Assert.True(activeFamilia.Id == familiaAtiva1.Id || activeFamilia.Id == familiaAtiva2.Id);
            
            Assert.NotNull(allFamilias);
            Assert.Equal(3, allFamilias.Count); // Should return all familias regardless of active status
        }
    }
}