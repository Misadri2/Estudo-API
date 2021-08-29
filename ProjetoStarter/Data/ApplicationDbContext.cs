using Microsoft.EntityFrameworkCore;
using ProjetoStarter.Models;

namespace ProjetoStarter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Starter> Starters {get; set;}
        public DbSet<Avaliacao> Avaliacoes {get; set;}
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base (options)
        {}
    }
}