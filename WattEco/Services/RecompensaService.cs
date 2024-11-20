using WattEco.DTOs;
using WattEco.Models;
using WattEco.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public class RecompensaService : IRecompensaService
    {
        private readonly IRecompensaRepository _recompensaRepository;

        // Injeção de dependência para o repositório
        public RecompensaService(IRecompensaRepository recompensaRepository)
        {
            _recompensaRepository = recompensaRepository;
        }

        // Método para buscar todas as recompensas
        public async Task<IEnumerable<RecompensaDTO>> GetAllRecompensasAsync()
        {
            // Recupera todas as recompensas e retorna diretamente como DTOs
            return await _recompensaRepository.GetAllRecompensasAsync();
        }

        // Método para buscar uma recompensa pelo ID
        public async Task<RecompensaDTO> GetRecompensaByIdAsync(int id)
        {
            // Recupera uma recompensa específica pelo ID e retorna como DTO
            return await _recompensaRepository.GetRecompensaByIdAsync(id);
        }

        // Método para adicionar uma nova recompensa
        public async Task AddRecompensaAsync(RecompensaDTO recompensaDTO)
        {
            // Adiciona a recompensa diretamente com base no DTO
            await _recompensaRepository.AddRecompensaAsync(recompensaDTO);
        }

        // Método para atualizar uma recompensa existente
        public async Task UpdateRecompensaAsync(RecompensaDTO recompensaDTO)
        {
            // Atualiza a recompensa diretamente usando o DTO
            await _recompensaRepository.UpdateRecompensaAsync(recompensaDTO);
        }

        // Método para deletar uma recompensa
        public async Task DeleteRecompensaAsync(int id)
        {
            // Remove a recompensa com base no ID
            await _recompensaRepository.DeleteRecompensaAsync(id);
        }
    }
}
