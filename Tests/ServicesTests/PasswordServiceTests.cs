using SisCras.Models.ValueObjects;
using SisCras.Services;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class PasswordServiceTests
    {
        private readonly PasswordService _service;

        public PasswordServiceTests()
        {
            _service = new PasswordService();
        }

        [Fact]
        public void CreatePassword_ShouldReturnPasswordHash()
        {
            // Act
            var result = _service.CreatePassword("password123");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Hash);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForValidPassword()
        {
            // Arrange
            var passwordHash = _service.CreatePassword("password123");

            // Act
            var result = _service.VerifyPassword("password123", passwordHash);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForInvalidPassword()
        {
            // Arrange
            var passwordHash = _service.CreatePassword("password123");

            // Act
            var result = _service.VerifyPassword("wrongpassword", passwordHash);

            // Assert
            Assert.False(result);
        }
    }
}