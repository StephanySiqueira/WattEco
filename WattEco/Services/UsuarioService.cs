using WattEco.DTOs;
using WattEco.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método para buscar todos os usuários
        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            // Recupera todos os usuários e retorna como DTOs
            return await _usuarioRepository.GetAllUsuariosAsync();
        }

        // Método para buscar um usuário pelo ID
        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            // Recupera um usuário específico pelo ID e retorna como DTO
            return await _usuarioRepository.GetUsuarioByIdAsync(id);
        }

        // Método para adicionar um novo usuário
        public async Task AddUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            // Adiciona um novo usuário com base no DTO
            await _usuarioRepository.AddUsuarioAsync(usuarioDTO);
        }

        // Método para atualizar um usuário existente
        public async Task UpdateUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            // Atualiza um usuário com base no DTO
            await _usuarioRepository.UpdateUsuarioAsync(usuarioDTO);
        }

        // Método para deletar um usuário
        public async Task DeleteUsuarioAsync(int id)
        {
            // Remove um usuário com base no ID
            await _usuarioRepository.DeleteUsuarioAsync(id);
        }
    }
}
