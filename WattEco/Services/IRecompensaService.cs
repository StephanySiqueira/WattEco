using WattEco.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public interface IRecompensaService
    {
        Task<IEnumerable<RecompensaDTO>> GetAllRecompensasAsync();
        Task<RecompensaDTO> GetRecompensaByIdAsync(int id);
        Task AddRecompensaAsync(RecompensaDTO recompensaDTO);
        Task UpdateRecompensaAsync(RecompensaDTO recompensaDTO);
        Task DeleteRecompensaAsync(int id);
    }
}
