using Moq;
using SisCras.Models;
using SisCras.Repositories;
using SisCras.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class ProntuarioServiceTests
    {
        private readonly Mock<IProntuarioRepository> _mockRepository;
        private readonly ProntuarioService _service;

        public ProntuarioServiceTests()
        {
            _mockRepository = new Mock<IProntuarioRepository>();
            _service = new ProntuarioService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Codigo = 1001, 
                DataCriacao = DateOnly.FromDateTime(DateTime.Now),
                FormaDeAcesso = "Espontânea"
            };
            _mockRepository.Setup(r => r.AddAsync(prontuario, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(prontuario));

            // Act
            var result = await _service.AddAsync(prontuario);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.AddAsync(prontuario, new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001, 
                DataCriacao = DateOnly.FromDateTime(DateTime.Now)
            };
            _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Prontuario?>(prontuario));

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.GetByIdAsync(1, new()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepository()
        {
            // Arrange
            var prontuarios = new List<Prontuario>
            {
                new Prontuario { Id = 1, Codigo = 1001, DataCriacao = DateOnly.FromDateTime(DateTime.Now) },
                new Prontuario { Id = 2, Codigo = 1002, DataCriacao = DateOnly.FromDateTime(DateTime.Now) },
                new Prontuario { Id = 3, Codigo = 1003, DataCriacao = DateOnly.FromDateTime(DateTime.Now) }
            };
            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ICollection<Prontuario>>(prontuarios));

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(3, result?.Count);
            _mockRepository.Verify(r => r.GetAllAsync(new()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001, 
                FormaDeAcesso = "Atualizada" 
            };
            _mockRepository.Setup(r => r.UpdateAsync(prontuario, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(prontuario);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(prontuario, new()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario { Id = 1, Codigo = 1001 };
            _mockRepository.Setup(r => r.DeleteAsync(prontuario, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(prontuario);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(prontuario, new()), Times.Once);
        }

        [Fact]
        public async Task GetFamiliaFromProntuario_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001,
                Familia = new Familia { Id = 1 }
            };
            _mockRepository.Setup(r => r.GetFamiliaFromProntuario(prontuario))
                .Returns(Task.FromResult(prontuario));

            // Act
            var result = await _service.GetFamiliaFromProntuario(prontuario);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.GetFamiliaFromProntuario(prontuario), Times.Once);
        }

        [Fact]
        public async Task GetFamiliaFromProntuario_ById_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001,
                Familia = new Familia { Id = 1 }
            };
            _mockRepository.Setup(r => r.GetFamiliaFromProntuario(1))
                .Returns(Task.FromResult(prontuario));

            // Act
            var result = await _service.GetFamiliaFromProntuario(1);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.GetFamiliaFromProntuario(1), Times.Once);
        }

        [Fact]
        public async Task GetFamiliaAndUsuariosFromProntuario_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001,
                Familia = new Familia 
                { 
                    Id = 1,
                    FamiliaUsuarios = new List<FamiliaUsuario>
                    {
                        new FamiliaUsuario { Usuario = new Usuario { Id = 1, Nome = "Usuario 1" } }
                    }
                }
            };
            _mockRepository.Setup(r => r.GetFamiliaAndUsuariosFromProntuario(prontuario))
                .Returns(Task.FromResult(prontuario));

            // Act
            var result = await _service.GetFamiliaAndUsuariosFromProntuario(prontuario);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.GetFamiliaAndUsuariosFromProntuario(prontuario), Times.Once);
        }

        [Fact]
        public async Task GetFamiliaAndUsuariosFromProntuario_ById_ShouldCallRepository()
        {
            // Arrange
            var prontuario = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001,
                Familia = new Familia 
                { 
                    Id = 1,
                    FamiliaUsuarios = new List<FamiliaUsuario>
                    {
                        new FamiliaUsuario { Usuario = new Usuario { Id = 1, Nome = "Usuario 1" } },
                        new FamiliaUsuario { Usuario = new Usuario { Id = 2, Nome = "Usuario 2" } }
                    }
                }
            };
            _mockRepository.Setup(r => r.GetFamiliaAndUsuariosFromProntuario(1))
                .Returns(Task.FromResult(prontuario));

            // Act
            var result = await _service.GetFamiliaAndUsuariosFromProntuario(1);

            // Assert
            Assert.Equal(prontuario, result);
            _mockRepository.Verify(r => r.GetFamiliaAndUsuariosFromProntuario(1), Times.Once);
        }

        [Fact]
        public async Task Prontuario_AtivoProperty_ShouldWorkCorrectly()
        {
            // Arrange
            var prontuarioAtivo = new Prontuario 
            { 
                Id = 1, 
                Codigo = 1001,
                DataSaida = null // Active prontuario
            };
            var prontuarioInativo = new Prontuario 
            { 
                Id = 2, 
                Codigo = 1002,
                DataSaida = DateOnly.FromDateTime(DateTime.Now) // Inactive prontuario
            };

            // Act & Assert
            Assert.True(prontuarioAtivo.Ativo);
            Assert.False(prontuarioInativo.Ativo);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyListWhenNoProntuarios()
        {
            // Arrange
            var emptyList = new List<Prontuario>();
            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ICollection<Prontuario>>(emptyList));

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(r => r.GetAllAsync(new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullForNonExistentProntuario()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Prontuario?>(null));

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(r => r.GetByIdAsync(999, new()), Times.Once);
        }
    }
}