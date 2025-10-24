using Moq;
using SisCras.Models;
using SisCras.Repositories;
using SisCras.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class CrasServiceTests
    {
        private readonly Mock<ICrasRepository> _mockRepository;
        private readonly CrasService _service;

        public CrasServiceTests()
        {
            _mockRepository = new Mock<ICrasRepository>();
            _service = new CrasService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Nome = "CRAS Test" };
            _mockRepository.Setup(r => r.AddAsync(cras)).ReturnsAsync(cras);

            // Act
            var result = await _service.AddAsync(cras);

            // Assert
            Assert.Equal(cras, result);
            _mockRepository.Verify(r => r.AddAsync(cras), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Id = 1, Nome = "CRAS Test" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cras);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(cras, result);
            _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepository()
        {
            // Arrange
            var crasList = new List<Cras>
            {
                new Cras { Id = 1, Nome = "CRAS 1" },
                new Cras { Id = 2, Nome = "CRAS 2" }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(crasList);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Id = 1, Nome = "CRAS Updated" };
            _mockRepository.Setup(r => r.UpdateAsync(cras)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(cras);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(cras), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Id = 1, Nome = "CRAS To Delete" };
            _mockRepository.Setup(r => r.DeleteAsync(cras)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(cras);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(cras), Times.Once);
        }

        [Fact]
        public async Task GetTecnicosFromCras_ShouldCallRepository()
        {
            // Arrange
            var tecnicos = new List<Tecnico>
            {
                new Tecnico { Id = 1, Nome = "Tecnico 1" }
            };
            _mockRepository.Setup(r => r.GetTecnicosFromCras(1)).ReturnsAsync(tecnicos);

            // Act
            var result = await _service.GetTecnicosFromCras(1);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetTecnicosFromCras(1), Times.Once);
        }

        [Fact]
        public async Task GetFamiliasFromCras_ShouldCallRepository()
        {
            // Arrange
            var familias = new List<Familia>
            {
                new Familia { Id = 1 }
            };
            _mockRepository.Setup(r => r.GetFamiliasFromCras(1)).ReturnsAsync(familias);

            // Act
            var result = await _service.GetFamiliasFromCras(1);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetFamiliasFromCras(1), Times.Once);
        }

        [Fact]
        public async Task GetProntuariosFromCras_ShouldCallRepository()
        {
            // Arrange
            var prontuarios = new List<Prontuario>
            {
                new Prontuario { Id = 1, Codigo = 1001 }
            };
            _mockRepository.Setup(r => r.GetProntuariosFromCras(1)).ReturnsAsync(prontuarios);

            // Act
            var result = await _service.GetProntuariosFromCras(1);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetProntuariosFromCras(1), Times.Once);
        }
    }
}