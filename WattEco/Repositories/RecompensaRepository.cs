using WattEco.DTOs;
using Microsoft.EntityFrameworkCore;
using WattEco.Persistence;
using WattEco.Models;

namespace WattEco.Repositories
{
    public class RecompensaRepository : IRecompensaRepository
    {
        private readonly OracleDbContext _context;

        public RecompensaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<RecompensaDTO> GetRecompensaByIdAsync(int id)
        {
            return await _context.Recompensas
                .Where(r => r.Id == id)
                .Select(r => new RecompensaDTO
                {
                    Id = r.Id,
                    Descricao = r.Descricao,
                    PontosNecessarios = r.PontosNecessarios,
                    UsuarioId = r.UsuarioId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RecompensaDTO>> GetAllRecompensasAsync()
        {
            return await _context.Recompensas
                .Select(r => new RecompensaDTO
                {
                    Id = r.Id,
                    Descricao = r.Descricao,
                    PontosNecessarios = r.PontosNecessarios,
                    UsuarioId = r.UsuarioId
                })
                .ToListAsync();
        }

        public async Task AddRecompensaAsync(RecompensaDTO recompensa)
        {
            var entity = new Recompensa
            {
                Descricao = recompensa.Descricao,
                PontosNecessarios = recompensa.PontosNecessarios,
                UsuarioId = recompensa.UsuarioId
            };
            _context.Recompensas.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecompensaAsync(RecompensaDTO recompensa)
        {
            var entity = await _context.Recompensas.FindAsync(recompensa.Id);
            if (entity != null)
            {
                entity.Descricao = recompensa.Descricao;
                entity.PontosNecessarios = recompensa.PontosNecessarios;
                entity.UsuarioId = recompensa.UsuarioId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRecompensaAsync(int id)
        {
            var entity = await _context.Recompensas.FindAsync(id);
            if (entity != null)
            {
                _context.Recompensas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
