using graphql_api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace graphql_api.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Director> Director { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
