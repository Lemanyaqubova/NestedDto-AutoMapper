using FirstApii.Data.Configurations;
using FirstApii.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApii.Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
