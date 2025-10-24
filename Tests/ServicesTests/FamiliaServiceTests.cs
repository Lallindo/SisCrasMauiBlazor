using Moq;
using SisCras.Models;
using SisCras.Models.Enums;
using SisCras.Repositories;
using SisCras.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class FamiliaServiceTests
    {
        private readonly Mock<IFamiliaRepository> _mockRepository;
        private readonly FamiliaService _service;

        public FamiliaServiceTests()
        {
            _mockRepository = new Mock<IFamiliaRepository>();
            _service = new FamiliaService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaNuclear };
            _mockRepository.Setup(r => r.AddAsync(familia, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(familia));

            // Act
            var result = await _service.AddAsync(familia);

            // Assert
            Assert.Equal(familia, result);
            _mockRepository.Verify(r => r.AddAsync(familia, new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { Id = 1, ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaNuclear };
            _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Familia?>(familia));

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(familia, result);
            _mockRepository.Verify(r => r.GetByIdAsync(1, new()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepository()
        {
            // Arrange
            var familias = new List<Familia>
            {
                new Familia { Id = 1, ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaNuclear },
                new Familia { Id = 2, ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.MonoparentalFeminina }
            };
            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ICollection<Familia>>(familias));

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result?.Count);
            _mockRepository.Verify(r => r.GetAllAsync(new()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { Id = 1, ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaNuclear };
            _mockRepository.Setup(r => r.UpdateAsync(familia, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(familia);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(familia, new()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { Id = 1, ConfiguracaoFamiliar = ConfiguracaoFamiliarEnum.FamiliaNuclear };
            _mockRepository.Setup(r => r.DeleteAsync(familia, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(familia);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(familia, new()), Times.Once);
        }

        [Fact]
        public async Task GetActiveUsuariosFromFamilia_ShouldCallRepository()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "Usuario Ativo" }
            };
            _mockRepository.Setup(r => r.GetActiveUsuariosFromFamilia(1))
                .Returns(Task.FromResult(usuarios));

            // Act
            var result = await _service.GetActiveUsuariosFromFamilia(1);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetActiveUsuariosFromFamilia(1), Times.Once);
        }

        [Fact]
        public async Task GetActiveUsuariosFromFamilia_WithFamiliaObject_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { Id = 1 };
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "Usuario Ativo" }
            };
            _mockRepository.Setup(r => r.GetActiveUsuariosFromFamilia(familia))
                .Returns(Task.FromResult(usuarios));

            // Act
            var result = await _service.GetActiveUsuariosFromFamilia(familia);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetActiveUsuariosFromFamilia(familia), Times.Once);
        }

        [Fact]
        public async Task GetUsuariosFromFamilia_ShouldCallRepository()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "Usuario 1" },
                new Usuario { Id = 2, Nome = "Usuario 2" }
            };
            _mockRepository.Setup(r => r.GetUsuariosFromFamilia(1))
                .Returns(Task.FromResult(usuarios));

            // Act
            var result = await _service.GetUsuariosFromFamilia(1);

            // Assert
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(r => r.GetUsuariosFromFamilia(1), Times.Once);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_ShouldCallRepository()
        {
            // Arrange
            var responsavel = new Usuario { Id = 1, Nome = "Responsavel" };
            _mockRepository.Setup(r => r.GetResponsavelFromFamilia(1))
                .Returns(Task.FromResult<Usuario?>(responsavel));

            // Act
            var result = await _service.GetResponsavelFromFamilia(1);

            // Assert
            Assert.Equal(responsavel, result);
            _mockRepository.Verify(r => r.GetResponsavelFromFamilia(1), Times.Once);
        }

        [Fact]
        public async Task GetResponsavelFromFamilia_ShouldReturnNullWhenNoResponsavel()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetResponsavelFromFamilia(1))
                .Returns(Task.FromResult<Usuario?>(null));

            // Act
            var result = await _service.GetResponsavelFromFamilia(1);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(r => r.GetResponsavelFromFamilia(1), Times.Once);
        }
    }
}