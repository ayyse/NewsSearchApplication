using NewsSearch.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace NewsSearch.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<News> News { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
    }
}
