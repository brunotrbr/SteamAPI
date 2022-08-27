using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;

namespace SteamAPI.Context
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {

        }

        public DbSet<Categories>? Categories { get; set; }
        public DbSet<Platforms>? Platforms { get; set; }
        public DbSet<Genres>? Genres { get; set; }
        public DbSet<SteamspyTags>? SteamspyTags { get; set; }
        public DbSet<Games>? Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
