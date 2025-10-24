using Moq;
using SisCras.Models;
using SisCras.Models.ValueObjects;
using SisCras.Repositories;
using SisCras.Services;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class TecnicoServiceTests
    {
        private readonly Mock<ITecnicoRepository> _mockTecnicoRepository;
        private readonly Mock<ILoggedUserService> _mockLoggedUserService;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly TecnicoService _service;

        public TecnicoServiceTests()
        {
            _mockTecnicoRepository = new Mock<ITecnicoRepository>();
            _mockLoggedUserService = new Mock<ILoggedUserService>();
            _mockPasswordService = new Mock<IPasswordService>();
            
            _service = new TecnicoService(
                _mockTecnicoRepository.Object,
                _mockLoggedUserService.Object,
                _mockPasswordService.Object);
        }

        [Fact]
        public async Task TryLoginAsync_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Login = "admin", Senha = "hashed_password" };
            var crasInfo = new CrasInfo(1, "CRAS Centro");
            
            _mockTecnicoRepository.Setup(r => r.GetTecnicoByLogin("admin"))
                .Returns(Task.FromResult<Tecnico?>(tecnico));
            _mockPasswordService.Setup(p => p.VerifyPassword("password", It.IsAny<PasswordHash>()))
                .Returns(true);
            _mockTecnicoRepository.Setup(r => r.GetCurrentCrasById(1))
                .Returns(Task.FromResult<CrasInfo?>(crasInfo));

            // Act
            var result = await _service.TryLoginAsync("admin", "password");

            // Assert
            Assert.True(result);
            _mockTecnicoRepository.Verify(r => r.GetTecnicoByLogin("admin"), Times.Once);
            _mockPasswordService.Verify(p => p.VerifyPassword("password", It.IsAny<PasswordHash>()), Times.Once);
            _mockTecnicoRepository.Verify(r => r.GetCurrentCrasById(1), Times.Once);
            _mockLoggedUserService.Verify(l => l.SetCurrentUser(tecnico), Times.Once);
        }

        [Fact]
        public async Task TryLoginAsync_ShouldReturnFalse_WhenTecnicoNotFound()
        {
            // Arrange
            _mockTecnicoRepository.Setup(r => r.GetTecnicoByLogin("unknown"))
                .Returns(Task.FromResult<Tecnico?>(null));

            // Act
            var result = await _service.TryLoginAsync("unknown", "password");

            // Assert
            Assert.False(result);
            _mockTecnicoRepository.Verify(r => r.GetTecnicoByLogin("unknown"), Times.Once);
            _mockPasswordService.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<PasswordHash>()), Times.Never);
            _mockLoggedUserService.Verify(l => l.SetCurrentUser(It.IsAny<Tecnico>()), Times.Never);
        }

        [Fact]
        public async Task TryLoginAsync_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Login = "admin", Senha = "hashed_password" };
            
            _mockTecnicoRepository.Setup(r => r.GetTecnicoByLogin("admin"))
                .Returns(Task.FromResult<Tecnico?>(tecnico));
            _mockPasswordService.Setup(p => p.VerifyPassword("wrongpassword", It.IsAny<PasswordHash>()))
                .Returns(false);

            // Act
            var result = await _service.TryLoginAsync("admin", "wrongpassword");

            // Assert
            Assert.False(result);
            _mockTecnicoRepository.Verify(r => r.GetTecnicoByLogin("admin"), Times.Once);
            _mockPasswordService.Verify(p => p.VerifyPassword("wrongpassword", It.IsAny<PasswordHash>()), Times.Once);
            _mockLoggedUserService.Verify(l => l.SetCurrentUser(It.IsAny<Tecnico>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            // Arrange
            var tecnico = new Tecnico { Nome = "Tecnico Test", Login = "test", Senha = "pass" };
            _mockTecnicoRepository.Setup(r => r.AddAsync(tecnico, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(tecnico));

            // Act
            var result = await _service.AddAsync(tecnico);

            // Assert
            Assert.Equal(tecnico, result);
            _mockTecnicoRepository.Verify(r => r.AddAsync(tecnico, new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Test", Login = "test" };
            _mockTecnicoRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Tecnico?>(tecnico));

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(tecnico, result);
            _mockTecnicoRepository.Verify(r => r.GetByIdAsync(1, new()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepository()
        {
            // Arrange
            var tecnicos = new List<Tecnico>
            {
                new Tecnico { Id = 1, Nome = "Tecnico 1", Login = "tec1" },
                new Tecnico { Id = 2, Nome = "Tecnico 2", Login = "tec2" }
            };
            _mockTecnicoRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ICollection<Tecnico>>(tecnicos));

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result?.Count);
            _mockTecnicoRepository.Verify(r => r.GetAllAsync(new()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Atualizado", Login = "updated" };
            _mockTecnicoRepository.Setup(r => r.UpdateAsync(tecnico, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(tecnico);

            // Assert
            _mockTecnicoRepository.Verify(r => r.UpdateAsync(tecnico, new()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Para Deletar", Login = "delete" };
            _mockTecnicoRepository.Setup(r => r.DeleteAsync(tecnico, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(tecnico);

            // Assert
            _mockTecnicoRepository.Verify(r => r.DeleteAsync(tecnico, new()), Times.Once);
        }
    }
}