using JuanDeDiosFrausto.Entidades;
using Microsoft.EntityFrameworkCore;

namespace JuanDeDiosFrausto
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
    }
}
