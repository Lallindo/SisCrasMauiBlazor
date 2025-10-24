using Microsoft.EntityFrameworkCore;
using SisCras.Models;
using SisCras.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.RepositoriesTests
{
    public class CrasRepository : BaseRepositoriesTests
    {
        private Repositories.CrasRepository _repository;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _repository = new Repositories.CrasRepository(_context);
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
    }
}