using Microsoft.EntityFrameworkCore;
using WattEco.DTOs;
using WattEco.Models;
using WattEco.Persistence;
using WattEco.Services;
using Microsoft.AspNetCore.Mvc;

namespace WattEco.Tests.Integration
{
    public class OracleDbContextIntegrationTests
    {
        private readonly OracleDbContext _context;

        public OracleDbContextIntegrationTests()
        {
            // Configuração do DbContext com banco de dados em memória para testes
            var options = new DbContextOptionsBuilder<OracleDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") 
                .Options;

            _context = new OracleDbContext(options);
        }

        [Fact]
        public async Task AddCliente_ShouldPersistUsuarioInDatabase()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario de Teste", Email = "teste@usuario.com", Senha = "teste123" };

            // Act
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Assert
            var savedUsuario = await _context.Usuarios.FindAsync(1);
            Assert.NotNull(savedUsuario);
            Assert.Equal("Usuario de Teste", savedUsuario.Nome); 
        }

        [Fact]
        public async Task GetCliente_ShouldReturnUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 2, Nome = "Usuario para Busca",Email = "busca@usuario.com", Senha = "busca123" };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var fetchedUsuario = await _context.Usuarios.FindAsync(2);

            // Assert
            Assert.NotNull(fetchedUsuario);
            Assert.Equal("Usuario para Busca", fetchedUsuario.Nome); 
        }
    }
}
