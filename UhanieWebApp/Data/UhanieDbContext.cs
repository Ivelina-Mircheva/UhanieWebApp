using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UhanieWebApp.Data
{
    public class UhanieDbContext : IdentityDbContext<Customer>
    {
        public UhanieDbContext(DbContextOptions<UhanieDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Bouquet> Bouquets { get; set; }
        public DbSet<OrderFlower> OrderFlowers { get; set; }
        public DbSet<OrderBouquet> OrderBouquets { get; set; }
    }
}