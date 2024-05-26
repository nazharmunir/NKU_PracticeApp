using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKU_PracticeApp.Models;

namespace SKU_PracticeApp.Repository
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated(); 
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 1000.99m, Discount = 10 },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 2000.99m, Discount = 15 },
                new Product { Id = 3, Name = "Product 3", Description = "Description 3", Price = 3000.99m, Discount = 20 }
            );
        }
    }
}
