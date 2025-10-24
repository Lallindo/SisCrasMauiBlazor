using SisCras.Models;
using SisCras.Services;
using System;
using Xunit;

namespace SisCras.Tests.ServicesTests
{
    public class LoggedUserServiceTests
    {
        private readonly LoggedUserService _service;

        public LoggedUserServiceTests()
        {
            _service = new LoggedUserService();
        }

        [Fact]
        public void GetCurrentUser_ShouldReturnNull_Initially()
        {
            // Act
            var result = _service.GetCurrentUser();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SetCurrentUser_ShouldSetUser()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Test" };

            // Act
            _service.SetCurrentUser(tecnico);

            // Assert
            Assert.Equal(tecnico, _service.GetCurrentUser());
            Assert.True(_service.IsUserLoggedIn);
        }

        [Fact]
        public void ClearCurrentUser_ShouldRemoveUser()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Test" };
            _service.SetCurrentUser(tecnico);

            // Act
            _service.ClearCurrentUser();

            // Assert
            Assert.Null(_service.GetCurrentUser());
            Assert.False(_service.IsUserLoggedIn);
        }

        [Fact]
        public void UserStateChanged_ShouldBeInvoked_WhenUserChanges()
        {
            // Arrange
            var tecnico = new Tecnico { Id = 1, Nome = "Tecnico Test" };
            var eventInvoked = false;
            _service.UserStateChanged += () => eventInvoked = true;

            // Act
            _service.SetCurrentUser(tecnico);

            // Assert
            Assert.True(eventInvoked);
        }
    }
}