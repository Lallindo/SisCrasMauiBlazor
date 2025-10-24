using Moq;
using SisCras.Models;
using SisCras.Repositories;
using SisCras.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepository;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _mockRepository = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Usuario Test", Cpf = "12345678901" };
            _mockRepository.Setup(r => r.AddAsync(usuario, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(usuario));

            // Act
            var result = await _service.AddAsync(usuario);

            // Assert
            Assert.Equal(usuario, result);
            _mockRepository.Verify(r => r.AddAsync(usuario, new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario Test", Cpf = "12345678901" };
            _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(usuario));

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(usuario, result);
            _mockRepository.Verify(r => r.GetByIdAsync(1, new()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepository()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "Usuario 1", Cpf = "11111111111" },
                new Usuario { Id = 2, Nome = "Usuario 2", Cpf = "22222222222" }
            };
            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ICollection<Usuario>>(usuarios));

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
            var usuario = new Usuario { Id = 1, Nome = "Usuario Atualizado", Cpf = "12345678901" };
            _mockRepository.Setup(r => r.UpdateAsync(usuario, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(usuario);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(usuario, new()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario Para Deletar", Cpf = "12345678901" };
            _mockRepository.Setup(r => r.DeleteAsync(usuario, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(usuario);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(usuario, new()), Times.Once);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_ShouldCallRepository()
        {
            // Arrange
            var familias = new List<Familia?>
            {
                new Familia { Id = 1 },
                new Familia { Id = 2 }
            };
            _mockRepository.Setup(r => r.GetFamiliasFromUsuario(1))
                .Returns(Task.FromResult(familias));

            // Act
            var result = await _service.GetFamiliasFromUsuario(1);

            // Assert
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(r => r.GetFamiliasFromUsuario(1), Times.Once);
        }

        [Fact]
        public async Task GetFamiliasFromUsuario_WithUsuarioObject_ShouldCallRepository()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario Test" };
            var familias = new List<Familia?>
            {
                new Familia { Id = 1 }
            };
            _mockRepository.Setup(r => r.GetFamiliasFromUsuario(usuario))
                .Returns(Task.FromResult(familias));

            // Act
            var result = await _service.GetFamiliasFromUsuario(usuario);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetFamiliasFromUsuario(usuario), Times.Once);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_ShouldCallRepository()
        {
            // Arrange
            var familia = new Familia { Id = 1 };
            _mockRepository.Setup(r => r.GetActiveFamiliaFromUsuario(1))
                .Returns(Task.FromResult<Familia?>(familia));

            // Act
            var result = await _service.GetActiveFamiliaFromUsuario(1);

            // Assert
            Assert.Equal(familia, result);
            _mockRepository.Verify(r => r.GetActiveFamiliaFromUsuario(1), Times.Once);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_ShouldReturnNullWhenNoActiveFamilia()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetActiveFamiliaFromUsuario(1))
                .Returns(Task.FromResult<Familia?>(null));

            // Act
            var result = await _service.GetActiveFamiliaFromUsuario(1);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(r => r.GetActiveFamiliaFromUsuario(1), Times.Once);
        }

        [Fact]
        public async Task GetActiveFamiliaFromUsuario_WithUsuarioObject_ShouldCallRepository()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario Test" };
            var familia = new Familia { Id = 1 };
            _mockRepository.Setup(r => r.GetActiveFamiliaFromUsuario(usuario))
                .Returns(Task.FromResult<Familia?>(familia));

            // Act
            var result = await _service.GetActiveFamiliaFromUsuario(usuario);

            // Assert
            Assert.Equal(familia, result);
            _mockRepository.Verify(r => r.GetActiveFamiliaFromUsuario(usuario), Times.Once);
        }
    }
}