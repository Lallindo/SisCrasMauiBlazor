using Moq;
using SisCras.Models;
using SisCras.Repositories;
using SisCras.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SisCras.Models.Enums;
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
            _mockRepository.Setup(r => r.AddAsync(cras, new()))
                .Returns(Task.FromResult(cras)); // Fixed

            // Act
            var result = await _service.AddAsync(cras);

            // Assert
            Assert.Equal(cras, result);
            _mockRepository.Verify(r => r.AddAsync(cras, new()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Id = 1, Nome = "CRAS Test" };
            _mockRepository.Setup(r => r.GetByIdAsync(1, new()))
                .Returns(Task.FromResult<Cras?>(cras)); // Fixed

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(cras, result);
            _mockRepository.Verify(r => r.GetByIdAsync(1, new()), Times.Once);
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
            _mockRepository.Setup(r => r.GetAllAsync(new()))
                .Returns(Task.FromResult<ICollection<Cras>>(crasList)); // Fixed

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
            var cras = new Cras { Id = 1, Nome = "CRAS Updated" };
            _mockRepository.Setup(r => r.UpdateAsync(cras, new()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(cras);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(cras, new()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            // Arrange
            var cras = new Cras { Id = 1, Nome = "CRAS To Delete" };
            _mockRepository.Setup(r => r.DeleteAsync(cras, new()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(cras);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(cras, new()), Times.Once);
        }

        [Fact]
        public async Task GetTecnicosFromCras_ShouldCallRepository()
        {
            // Arrange
            var tecnicos = new List<Tecnico>
            {
                new Tecnico { Id = 1, Nome = "Tecnico 1" }
            };
            _mockRepository.Setup(r => r.GetTecnicosFromCras(1))
                .Returns(Task.FromResult(tecnicos)); // Fixed

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
            _mockRepository.Setup(r => r.GetFamiliasFromCras(1))
                .Returns(Task.FromResult(familias)); // Fixed

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
            _mockRepository.Setup(r => r.GetProntuariosFromCras(1))
                .Returns(Task.FromResult(prontuarios)); // Fixed

            // Act
            var result = await _service.GetProntuariosFromCras(1);

            // Assert
            Assert.Single(result);
            _mockRepository.Verify(r => r.GetProntuariosFromCras(1), Times.Once);
        }
        // Add these test methods to the CrasServiceTests class

[Fact]
public async Task GetProntuarioAndFamiliaAndUsuariosFromCras_ShouldCallRepository()
{
    // Arrange
    var prontuarios = new List<Prontuario>
    {
        new Prontuario 
        { 
            Id = 1, 
            Codigo = 1001,
            Familia = new Familia 
            { 
                Id = 1,
                FamiliaUsuarios = new List<FamiliaUsuario>
                {
                    new FamiliaUsuario 
                    { 
                        Id = 1,
                        Usuario = new Usuario { Id = 1, Nome = "Usuario Teste", Cpf = "111.111.111-11" },
                        Parentesco = ParentescoEnum.Responsavel,
                        Ativo = true
                    }
                }
            }
        }
    };

    _mockRepository.Setup(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1))
        .ReturnsAsync(prontuarios);

    // Act
    var result = await _service.GetProntuarioAndFamiliaAndUsuariosFromCras(1);

    // Assert
    Assert.NotNull(result);
    Assert.Single(result);
    Assert.Equal("Usuario Teste", result[0].Familia.FamiliaUsuarios.First().Usuario.Nome);
    _mockRepository.Verify(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1), Times.Once);
}

[Fact]
public async Task GetProntuarioAndFamiliaAndUsuariosFromCras_ShouldReturnEmptyListWhenNoData()
{
    // Arrange
    var prontuariosVazios = new List<Prontuario>();
    _mockRepository.Setup(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1))
        .ReturnsAsync(prontuariosVazios);

    // Act
    var result = await _service.GetProntuarioAndFamiliaAndUsuariosFromCras(1);

    // Assert
    Assert.NotNull(result);
    Assert.Empty(result);
    _mockRepository.Verify(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1), Times.Once);
}

[Fact]
public async Task GetProntuarioAndFamiliaAndUsuariosFromCras_ShouldReturnCompleteDataStructure()
{
    // Arrange
    var prontuariosCompletos = new List<Prontuario>
    {
        new Prontuario 
        { 
            Id = 1,
            Codigo = 1001,
            Familia = new Familia 
            { 
                Id = 1,
                FamiliaUsuarios = new List<FamiliaUsuario>
                {
                    new FamiliaUsuario 
                    { 
                        Id = 1,
                        Usuario = new Usuario { Id = 1, Nome = "Responsavel", Cpf = "111.111.111-11" },
                        Parentesco = ParentescoEnum.Responsavel,
                        Ativo = true
                    },
                    new FamiliaUsuario 
                    { 
                        Id = 2,
                        Usuario = new Usuario { Id = 2, Nome = "Filho", Cpf = "222.222.222-22" },
                        Parentesco = ParentescoEnum.Filho,
                        Ativo = true
                    }
                }
            }
        },
        new Prontuario 
        { 
            Id = 2,
            Codigo = 1002,
            Familia = new Familia 
            { 
                Id = 2,
                FamiliaUsuarios = new List<FamiliaUsuario>
                {
                    new FamiliaUsuario 
                    { 
                        Id = 3,
                        Usuario = new Usuario { Id = 3, Nome = "Conjuge", Cpf = "333.333.333-33" },
                        Parentesco = ParentescoEnum.Conjuge,
                        Ativo = true
                    }
                }
            }
        }
    };

    _mockRepository.Setup(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1))
        .ReturnsAsync(prontuariosCompletos);

    // Act
    var result = await _service.GetProntuarioAndFamiliaAndUsuariosFromCras(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count);
    
    var primeiroProntuario = result[0];
    Assert.NotNull(primeiroProntuario.Familia);
    Assert.Equal(2, primeiroProntuario.Familia.FamiliaUsuarios.Count);
    Assert.Contains(primeiroProntuario.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Responsavel");
    Assert.Contains(primeiroProntuario.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Filho");
    
    var segundoProntuario = result[1];
    Assert.NotNull(segundoProntuario.Familia);
    Assert.Single(segundoProntuario.Familia.FamiliaUsuarios);
    Assert.Contains(segundoProntuario.Familia.FamiliaUsuarios, fu => fu.Usuario.Nome == "Conjuge");
    
    _mockRepository.Verify(r => r.GetProntuarioAndFamiliaAndUsuariosFromCras(1), Times.Once);
}
    }
}