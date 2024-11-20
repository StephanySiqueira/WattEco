using WattEco.DTOs;
using WattEco.Models;
using WattEco.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WattEco.Repositories
{
    public class MissaoRepository : IMissaoRepository
    {
        private readonly OracleDbContext _context;

        public MissaoRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MissaoDTO>> GetAllMissoesAsync()
        {
            return await _context.Missoes
                .Include(m => m.Usuario) 
                .Select(m => new MissaoDTO
                {
                    Id = m.Id,
                    Descricao = m.Descricao,
                    Pontuacao = m.Pontuacao,
                    UsuarioId = m.UsuarioId,
                })
                .ToListAsync();
        }

        public async Task<MissaoDTO> GetMissaoByIdAsync(int id)
        {
            return await _context.Missoes
                .Where(m => m.Id == id)
                .Include(m => m.Usuario)
                .Select(m => new MissaoDTO
                {
                    Id = m.Id,
                    Descricao = m.Descricao,
                    Pontuacao = m.Pontuacao,
                    UsuarioId = m.UsuarioId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddMissaoAsync(MissaoDTO missaoDTO)
        {
            var missao = new Missao
            {
                Descricao = missaoDTO.Descricao,
                Pontuacao = missaoDTO.Pontuacao,
                UsuarioId = missaoDTO.UsuarioId
            };

            _context.Missoes.Add(missao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMissaoAsync(MissaoDTO missaoDTO)
        {
            var missao = await _context.Missoes.FindAsync(missaoDTO.Id);
            if (missao == null) throw new KeyNotFoundException();

            missao.Descricao = missaoDTO.Descricao;
            missao.Pontuacao = missaoDTO.Pontuacao;
            missao.UsuarioId = missaoDTO.UsuarioId;

            _context.Entry(missao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMissaoAsync(int id)
        {
            var missao = await _context.Missoes.FindAsync(id);
            if (missao == null) throw new KeyNotFoundException();

            _context.Missoes.Remove(missao);
            await _context.SaveChangesAsync();
        }
    }
}

