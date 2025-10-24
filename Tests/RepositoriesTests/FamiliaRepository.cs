using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public class FamiliaRepository : BaseRepositoriesTests
    {
        private Repositories.FamiliaRepository _repository;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _repository = new Repositories.FamiliaRepository(_context);
            await ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddFamilia()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.MonoparentalFeminina);

            // Act
            var result = await _repository.AddAsync(familia);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ConfiguracaoFamiliarEnum.MonoparentalFeminina, result.ConfiguracaoFamiliar);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFamilia()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaNuclear);
            await _repository.AddAsync(familia);

            // Act
            var result = await _repository.GetByIdAsync(familia.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familia.Id, result.Id);
            Assert.Equal(ConfiguracaoFamiliarEnum.FamiliaNuclear, result.ConfiguracaoFamiliar);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullForNonExistentFamilia()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllFamilias()
        {
            // Arrange
            await _repository.AddAsync(ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaNuclear));
            await _repository.AddAsync(ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.MonoparentalMasculina));
            await _repository.AddAsync(ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaExtensa));

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyListWhenNoFamilias()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateFamilia()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaNuclear);
            await _repository.AddAsync(familia);

            // Act
            familia.ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaHomoafetiva;
            await _repository.UpdateAsync(familia);

            // Assert
            var updatedFamilia = await _repository.GetByIdAsync(familia.Id);
            Assert.NotNull(updatedFamilia);
            Assert.Equal(ConfiguracaoFamiliarEnum.FamiliaHomoafetiva, updatedFamilia.ConfiguracaoFamiliar);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveFamilia()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia(ConfiguracaoFamiliarEnum.FamiliaNuclear);
            await _repository.AddAsync(familia);
    
            // Verify it was added
            var initialFamilia = await _repository.GetByIdAsync(familia.Id);
            Assert.NotNull(initialFamilia);

            // Act
            await _repository.DeleteAsync(familia);

            // Assert - Use a fresh repository/context to avoid tracking issues
            var deletedFamilia = await _repository.GetByIdAsync(familia.Id);
            Assert.Null(deletedFamilia);
    
            // Also check that it's not in the GetAll list
            var allFamilias = await _repository.GetAllAsync();
            Assert.DoesNotContain(allFamilias, f => f.Id == familia.Id);
        }

        [Fact]
        public async Task GetActiveUsuariosFromFamilia_ShouldReturnActiveUsuarios()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var usuario = ModelFactory.CreateUsuario("Maria", "12345678901", "12345678901");
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Filho, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Debug: Check if data was saved correctly
            var savedFamiliaUsuario = await _context.FamiliaUsuarios
                .FirstOrDefaultAsync();
            Assert.NotNull(savedFamiliaUsuario); // This should pass
            Assert.True(savedFamiliaUsuario.Ativo); // This should pass

            var savedUsuario = await _context.Usuarios.FindAsync(usuario.Id);
            Assert.NotNull(savedUsuario); // This should pass

            // Act
            var usuarios = await _repository.GetActiveUsuariosFromFamilia(familia.Id);

            // Debug: Check what was returned
            Assert.NotNull(usuarios); // This should pass
            Assert.NotEmpty(usuarios); // If this fails, the query isn't working

            // Assert
            Assert.Single(usuarios);
            Assert.Equal("Maria", usuarios.First().Nome);
        }

        [Fact]
        public async Task GetActiveUsuariosFromFamilia_WithFamiliaObject_ShouldReturnActiveUsuarios()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var usuario = ModelFactory.CreateUsuario("Carlos", "12345678902", "12345678902");
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Filho, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var usuarios = await _repository.GetActiveUsuariosFromFamilia(familia);

            // Assert
            Assert.NotNull(usuarios);
            Assert.Single(usuarios);
            Assert.Equal("Carlos", usuarios.First().Nome);
        }

        [Fact]
        public async Task GetActiveUsuariosFromFamilia_ShouldReturnOnlyActiveUsuarios()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var usuarioAtivo = ModelFactory.CreateUsuario("Ativo", "11111111111", "11111111111");
            var usuarioInativo = ModelFactory.CreateUsuario("Inativo", "22222222222", "22222222222");
            _context.Usuarios.AddRange(usuarioAtivo, usuarioInativo);
            await _context.SaveChangesAsync();

            var familiaUsuarioAtivo = ModelFactory.CreateFamiliaUsuario(familia.Id, usuarioAtivo.Id, ParentescoEnum.Filho, true);
            var familiaUsuarioInativo = ModelFactory.CreateFamiliaUsuario(familia.Id, usuarioInativo.Id, ParentescoEnum.Conjuge, false);
            _context.FamiliaUsuarios.AddRange(familiaUsuarioAtivo, familiaUsuarioInativo);
            await _context.SaveChangesAsync();

            // Act
            var usuarios = await _repository.GetActiveUsuariosFromFamilia(familia.Id);

            // Assert
            Assert.NotNull(usuarios);
            Assert.Single(usuarios);
            Assert.Equal("Ativo", usuarios.First().Nome);
        }

        [Fact]
        public async Task GetUsuariosFromFamilia_ShouldReturnAllUsuarios()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var usuario1 = ModelFactory.CreateUsuario("Usuario 1", "11111111111", "11111111111");
            var usuario2 = ModelFactory.CreateUsuario("Usuario 2", "22222222222", "22222222222");
            _context.Usuarios.AddRange(usuario1, usuario2);
            await _context.SaveChangesAsync();

            var familiaUsuario1 = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario1.Id, ParentescoEnum.Filho, true);
            var familiaUsuario2 = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario2.Id, ParentescoEnum.Conjuge, false);
            _context.FamiliaUsuarios.AddRange(familiaUsuario1, familiaUsuario2);
            await _context.SaveChangesAsync();

            // Act
            var usuarios = await _repository.GetUsuariosFromFamilia(familia.Id);

            // Assert
            Assert.NotNull(usuarios);
            Assert.Equal(2, usuarios.Count);
        }

        [Fact]
        public async Task GetUsuariosFromFamilia_WithFamiliaObject_ShouldReturnAllUsuarios()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var usuario = ModelFactory.CreateUsuario("Usuario", "11111111111", "11111111111");
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(familia.Id, usuario.Id, ParentescoEnum.Responsavel, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var usuarios = await _repository.GetUsuariosFromFamilia(familia);

            // Assert
            Assert.NotNull(usuarios);
            Assert.Single(usuarios);
            Assert.Equal("Usuario", usuarios.First().Nome);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_ShouldReturnResponsavel()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var responsavel = ModelFactory.CreateUsuario("Carlos", "12345678902", "12345678902");
            _context.Usuarios.Add(responsavel);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(
                familia.Id, responsavel.Id, ParentescoEnum.Responsavel, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetResponsavelFromFamilia(familia.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Carlos", result.Nome);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_WithFamiliaObject_ShouldReturnResponsavel()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var responsavel = ModelFactory.CreateUsuario("Ana", "12345678903", "12345678903");
            _context.Usuarios.Add(responsavel);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(
                familia.Id, responsavel.Id, ParentescoEnum.Responsavel, true);
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetResponsavelFromFamilia(familia);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ana", result.Nome);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_ShouldReturnNullWhenNoResponsavel()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            // Act
            var result = await _repository.GetResponsavelFromFamilia(familia.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_ShouldReturnNullWhenResponsavelInactive()
        {
            // Arrange
            var familia = ModelFactory.CreateFamilia();
            await _repository.AddAsync(familia);

            var responsavel = ModelFactory.CreateUsuario("Inativo", "11111111111", "11111111111");
            _context.Usuarios.Add(responsavel);
            await _context.SaveChangesAsync();

            var familiaUsuario = ModelFactory.CreateFamiliaUsuario(
                familia.Id, responsavel.Id, ParentescoEnum.Responsavel, false); // Inactive
            _context.FamiliaUsuarios.Add(familiaUsuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetResponsavelFromFamilia(familia.Id);

            // Assert
            Assert.Null(result);
        }
    }
}