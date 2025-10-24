using SisCras.Models;
using SisCras.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public class ProntuarioRepositoryTests : BaseRepositoriesTests
    {
        private Repositories.ProntuarioRepository _repository;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _repository = new Repositories.ProntuarioRepository(_context);
            await ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddProntuario()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001, "Encaminhamento");

            // Act
            var result = await _repository.AddAsync(prontuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1001, result.Codigo);
            Assert.Equal("Encaminhamento", result.FormaDeAcesso);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProntuario()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001);
            await _repository.AddAsync(prontuario);

            // Act
            var result = await _repository.GetByIdAsync(prontuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(prontuario.Id, result.Id);
            Assert.Equal(1001, result.Codigo);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullForNonExistentProntuario()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProntuarios()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            await _repository.AddAsync(ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001));
            await _repository.AddAsync(ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1002));
            await _repository.AddAsync(ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1003));

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyListWhenNoProntuarios()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProntuario()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001, "Original");
            await _repository.AddAsync(prontuario);

            // Act
            prontuario.Codigo = 2001;
            prontuario.FormaDeAcesso = "Atualizada";
            await _repository.UpdateAsync(prontuario);

            // Assert
            var updatedProntuario = await _repository.GetByIdAsync(prontuario.Id);
            Assert.NotNull(updatedProntuario);
            Assert.Equal(2001, updatedProntuario.Codigo);
            Assert.Equal("Atualizada", updatedProntuario.FormaDeAcesso);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProntuario()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            var prontuario = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001);
            await _repository.AddAsync(prontuario);
            var initialCount = (await _repository.GetAllAsync()).Count;

            // Act
            await _repository.DeleteAsync(prontuario);

            // Assert
            var finalCount = (await _repository.GetAllAsync()).Count;
            var deletedProntuario = await _repository.GetByIdAsync(prontuario.Id);
            
            Assert.Null(deletedProntuario);
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public async Task NotImplementedMethods_ShouldThrowNotImplementedException()
        {
            // Arrange
            var prontuario = new Prontuario();

            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => 
                _repository.GetFamiliaFromProntuario(prontuario));
            await Assert.ThrowsAsync<NotImplementedException>(() => 
                _repository.GetFamiliaFromProntuario(1));
            await Assert.ThrowsAsync<NotImplementedException>(() => 
                _repository.GetFamiliaAndUsuariosFromProntuario(prontuario));
            await Assert.ThrowsAsync<NotImplementedException>(() => 
                _repository.GetFamiliaAndUsuariosFromProntuario(1));
        }

        [Fact]
        public async Task Prontuario_AtivoProperty_ShouldReturnCorrectValue()
        {
            // Arrange
            var cras = ModelFactory.CreateCras();
            _context.Cras.Add(cras);

            var familia = ModelFactory.CreateFamilia();
            _context.Familias.Add(familia);

            var tecnico = ModelFactory.CreateTecnico();
            _context.Tecnicos.Add(tecnico);

            await _context.SaveChangesAsync();

            var prontuarioAtivo = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1001);
            var prontuarioInativo = ModelFactory.CreateProntuario(cras.Id, familia.Id, tecnico.Id, 1002);
            prontuarioInativo.DataSaida = DateOnly.FromDateTime(DateTime.Now);

            // Act & Assert
            Assert.True(prontuarioAtivo.Ativo);
            Assert.False(prontuarioInativo.Ativo); // DataSaida has value
        }
    }
}