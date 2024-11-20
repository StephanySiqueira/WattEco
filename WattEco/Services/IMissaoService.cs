using WattEco.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public interface IMissaoService
    {
        Task<IEnumerable<MissaoDTO>> GetAllMissoesAsync();
        Task<MissaoDTO> GetMissaoByIdAsync(int id);
        Task AddMissaoAsync(MissaoDTO missaoDTO);
        Task UpdateMissaoAsync(MissaoDTO missaoDTO);
        Task DeleteMissaoAsync(int id);
    }
}
