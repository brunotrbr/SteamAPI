using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;

namespace SteamAPI.Context
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {
        }

        public DbSet<Games> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
