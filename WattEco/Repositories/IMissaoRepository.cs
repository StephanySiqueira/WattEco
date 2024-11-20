using WattEco.DTOs;

namespace WattEco.Repositories
{
    public interface IMissaoRepository
    {
        Task<IEnumerable<MissaoDTO>> GetAllMissoesAsync();
        Task<MissaoDTO> GetMissaoByIdAsync(int id);
        Task AddMissaoAsync(MissaoDTO missaoDTO);
        Task UpdateMissaoAsync(MissaoDTO missaoDTO);
        Task DeleteMissaoAsync(int id);
    }
}
