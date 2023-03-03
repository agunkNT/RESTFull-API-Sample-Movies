using API_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> opts) : base(opts)
        {
        }

        public DbSet<MovieModel> Movies { get; set; }
    }
}
