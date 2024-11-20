using WattEco.DTOs;

namespace WattEco.Repositories
{
    public interface IRecompensaRepository
    {
        Task<RecompensaDTO> GetRecompensaByIdAsync(int id);
        Task<IEnumerable<RecompensaDTO>> GetAllRecompensasAsync();
        Task AddRecompensaAsync(RecompensaDTO recompensa);
        Task UpdateRecompensaAsync(RecompensaDTO recompensa);
        Task DeleteRecompensaAsync(int id);
    }
}
