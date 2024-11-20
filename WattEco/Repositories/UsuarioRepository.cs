using WattEco.DTOs;
using WattEco.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WattEco.Persistence;

namespace WattEco.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly OracleDbContext _context;

        public UsuarioRepository(OracleDbContext context)
        {
            _context = context;
        }

        // Retorna todos os usuários como DTOs
        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Senha = u.Senha
                })
                .ToListAsync();
        }

        // Retorna um usuário específico como DTO
        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Senha = u.Senha
                })
                .FirstOrDefaultAsync();
        }

        // Adiciona um novo usuário
        public async Task AddUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha 
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        // Atualiza um usuário existente
        public async Task UpdateUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioDTO.Id);
            if (usuario != null)
            {
                usuario.Nome = usuarioDTO.Nome;
                usuario.Email = usuarioDTO.Email;
                usuario.Senha = usuarioDTO.Senha; 

                await _context.SaveChangesAsync();
            }
        }

        // Deleta um usuário pelo ID
        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
