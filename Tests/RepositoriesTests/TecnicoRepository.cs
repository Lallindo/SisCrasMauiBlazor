using SisCras.Models;
using SisCras.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public class TecnicoRepository : BaseRepositoriesTests
    {
        private Repositories.TecnicoRepository _repository;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _repository = new Repositories.TecnicoRepository(_context);
            await ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddTecnico()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Ana", "ana", "senha123");

            // Act
            var result = await _repository.AddAsync(tecnico);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ana", result.Nome);
            Assert.Equal("ana", result.Login);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTecnico()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("João", "joao", "senha123");
            await _repository.AddAsync(tecnico);

            // Act
            var result = await _repository.GetByIdAsync(tecnico.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tecnico.Id, result.Id);
            Assert.Equal("João", result.Nome);
            Assert.Equal("joao", result.Login);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullForNonExistentTecnico()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTecnicos()
        {
            // Arrange
            await _repository.AddAsync(ModelFactory.CreateTecnico("Tecnico 1", "tec1", "senha1"));
            await _repository.AddAsync(ModelFactory.CreateTecnico("Tecnico 2", "tec2", "senha2"));
            await _repository.AddAsync(ModelFactory.CreateTecnico("Tecnico 3", "tec3", "senha3"));

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyListWhenNoTecnicos()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTecnico()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Tecnico Original", "original", "senha123");
            await _repository.AddAsync(tecnico);

            // Act
            tecnico.Nome = "Tecnico Atualizado";
            tecnico.Login = "atualizado";
            await _repository.UpdateAsync(tecnico);

            // Assert
            var updatedTecnico = await _repository.GetByIdAsync(tecnico.Id);
            Assert.NotNull(updatedTecnico);
            Assert.Equal("Tecnico Atualizado", updatedTecnico.Nome);
            Assert.Equal("atualizado", updatedTecnico.Login);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveTecnico()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Tecnico Para Deletar", "deletar", "senha123");
            await _repository.AddAsync(tecnico);
            var initialCount = (await _repository.GetAllAsync()).Count;

            // Act
            await _repository.DeleteAsync(tecnico);

            // Assert
            var finalCount = (await _repository.GetAllAsync()).Count;
            var deletedTecnico = await _repository.GetByIdAsync(tecnico.Id);
            
            Assert.Null(deletedTecnico);
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public async Task GetTecnicoByLoginAsync_ShouldReturnTecnico()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Ana", "ana", "senha123");
            await _repository.AddAsync(tecnico);

            // Act
            var result = await _repository.GetTecnicoByLogin("ana");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ana", result.Nome);
            Assert.Equal("ana", result.Login);
        }

        [Fact]
        public async Task GetTecnicoByLoginAsync_ShouldReturnNullForNonExistentLogin()
        {
            // Act
            var result = await _repository.GetTecnicoByLogin("inexistente");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCurrentCrasByIdAsync_ShouldReturnCurrentCras()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Ana", "ana", "senha123");
            await _repository.AddAsync(tecnico);

            var cras = ModelFactory.CreateCras("CRAS Centro");
            _context.Cras.Add(cras);
            await _context.SaveChangesAsync();

            var tecnicoCras = ModelFactory.CreateTecnicoCras(tecnico.Id, cras.Id);
            _context.TecnicoCras.Add(tecnicoCras);
            await _context.SaveChangesAsync();

            // Act
            var crasInfo = await _repository.GetCurrentCrasById(tecnico.Id);

            // Assert
            Assert.NotNull(crasInfo);
            Assert.Equal(cras.Id, crasInfo.Id);
            Assert.Equal("CRAS Centro", crasInfo.Nome);
        }

        [Fact]
        public async Task GetCurrentCrasByIdAsync_ShouldReturnNullWhenNoCurrentCras()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Tecnico Sem CRAS", "semcras", "senha123");
            await _repository.AddAsync(tecnico);

            // Act
            var crasInfo = await _repository.GetCurrentCrasById(tecnico.Id);

            // Assert
            Assert.Null(crasInfo);
        }

        [Fact]
        public async Task GetCurrentCrasByIdAsync_ShouldReturnNullWhenTecnicoHasDataSaida()
        {
            // Arrange
            var tecnico = ModelFactory.CreateTecnico("Tecnico Inativo", "inativo", "senha123");
            await _repository.AddAsync(tecnico);

            var cras = ModelFactory.CreateCras("CRAS Centro");
            _context.Cras.Add(cras);
            await _context.SaveChangesAsync();

            var tecnicoCras = ModelFactory.CreateTecnicoCras(
                tecnico.Id, cras.Id, 
                dataSaida: DateOnly.FromDateTime(System.DateTime.Now)); // With end date
            _context.TecnicoCras.Add(tecnicoCras);
            await _context.SaveChangesAsync();

            // Act
            var crasInfo = await _repository.GetCurrentCrasById(tecnico.Id);

            // Assert
            Assert.Null(crasInfo);
        }
    }
}