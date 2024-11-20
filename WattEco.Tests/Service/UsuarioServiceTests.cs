using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WattEco.DTOs;
using WattEco.Repositories;
using WattEco.Services;
using Xunit;

namespace WattEco.Tests.Service
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            // Cria o mock do repositório
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();

            // Cria uma instância do UsuarioService com o repositório mockado
            _usuarioService = new UsuarioService(_mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task GetAllUsuariosAsync_ReturnsListOfUsuarios()
        {
            // Arrange: Define o que o mock deve retornar
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO { Id = 1, Nome = "Usuario 1", Email = "usuario1@example.com" },
                new UsuarioDTO { Id = 2, Nome = "Usuario 2", Email = "usuario2@example.com" }
            };

            _mockUsuarioRepository.Setup(repo => repo.GetAllUsuariosAsync()).ReturnsAsync(usuarios);

            // Act: Chama o método a ser testado
            var result = await _usuarioService.GetAllUsuariosAsync();

            // Assert: Verifica se o resultado é o esperado
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Usuario 1", result.First().Nome);
            Assert.Equal("Usuario 2", result.Last().Nome);
        }
    }
}
