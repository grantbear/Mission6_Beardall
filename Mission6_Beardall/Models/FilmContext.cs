using Microsoft.EntityFrameworkCore;

namespace Mission6_Beardall.Models
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options) : base (options) 
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
