using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Persistence
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }


        public DbSet<Product> Products => Set<Product>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                //e.Property(p => p.Name).HasMaxLength(150).IsRequired();
                //e.Property(p => p.Price).HasPrecision(18, 2);
                //e.Property(p => p.DiscountPercent).HasPrecision(5, 2);
                //e.Property(p => p.Description).HasMaxLength(500);
               
            });


            // Seed sample data
            modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1,  Name = "Laptop 14\"", Description = "Core i7, 16GB RAM, 512GB SSD", Price = 35000m, DiscountPercent = 10 },
            new Product { Id = 2, Name = "Smartphone Pro", Description = "128GB, OLED", Price = 18000m, DiscountPercent=5 },
            new Product { Id = 3,  Name = "Headphones", Description = "Noise Cancelling", Price = 2500m, DiscountPercent = 30 },
            new Product { Id = 4,  Name = "Wireless Mouse", Description = "Ergonomic", Price = 600m, DiscountPercent = 2 }
            );
        }
    }
}
