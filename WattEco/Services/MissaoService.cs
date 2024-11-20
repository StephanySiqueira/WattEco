using WattEco.DTOs;
using WattEco.Models;
using WattEco.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Services
{
    public class MissaoService : IMissaoService
    {
        private readonly IMissaoRepository _missaoRepository;

        // Injeção de dependência para o repositório
        public MissaoService(IMissaoRepository missaoRepository)
        {
            _missaoRepository = missaoRepository;
        }

        // Método para buscar todas as missões
        public async Task<IEnumerable<MissaoDTO>> GetAllMissoesAsync()
        {
            // Recupera todas as missões e retorna diretamente como DTOs
            return await _missaoRepository.GetAllMissoesAsync();
        }

        // Método para buscar uma missão pelo ID
        public async Task<MissaoDTO> GetMissaoByIdAsync(int id)
        {
            // Recupera uma missão específica pelo ID e retorna como DTO
            return await _missaoRepository.GetMissaoByIdAsync(id);
        }

        // Método para adicionar uma nova missão
        public async Task AddMissaoAsync(MissaoDTO missaoDTO)
        {
            // Adiciona a missão diretamente com base no DTO
            await _missaoRepository.AddMissaoAsync(missaoDTO);
        }

        // Método para atualizar uma missão existente
        public async Task UpdateMissaoAsync(MissaoDTO missaoDTO)
        {
            // Atualiza a missão diretamente usando o DTO
            await _missaoRepository.UpdateMissaoAsync(missaoDTO);
        }

        // Método para deletar uma missão
        public async Task DeleteMissaoAsync(int id)
        {
            // Remove a missão com base no ID
            await _missaoRepository.DeleteMissaoAsync(id);
        }
    }
}
