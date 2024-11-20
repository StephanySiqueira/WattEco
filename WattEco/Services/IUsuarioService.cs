using WattEco.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(UsuarioDTO usuarioDTO);
        Task UpdateUsuarioAsync(UsuarioDTO usuarioDTO);
        Task DeleteUsuarioAsync(int id);
    }
}
