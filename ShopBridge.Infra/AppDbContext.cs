using Microsoft.EntityFrameworkCore;
using ShopBridge.Core;

namespace ShopBridge.Infra
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Product> Products { get; set; }       
    }
}
