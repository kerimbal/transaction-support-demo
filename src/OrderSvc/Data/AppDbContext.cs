using Microsoft.EntityFrameworkCore;
using OrderSvc.Entity;
namespace OrderSvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerOrderHistory> CustomerOrderHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(x => x.Price).HasPrecision(18, 4);
                entity.Property(x => x.CustomerName).IsRequired();
            });

            modelBuilder.Entity<CustomerOrderHistory>(entity =>
            {
                entity.Property(x => x.Price).HasPrecision(18, 4);
                entity.Property(x => x.CustomerName).IsRequired();
            });
        }
    }
}
