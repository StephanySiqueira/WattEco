using Microsoft.EntityFrameworkCore;
using WattEco.Models;

namespace WattEco.Persistence
{
    public class OracleDbContext : DbContext 
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Missao> Missoes { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }

        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {
        }
    }
}
