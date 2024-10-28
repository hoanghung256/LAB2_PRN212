using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repository
{
    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext() { }
        public DbSet<Category> Categories { get; set; } = null;
        public DbSet<AccountMember> AccountMembers { get; set; } = null;
        public DbSet<Product> Products { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category(1, "Beverages"),
                    new Category(2, "Condiments"),
                    new Category(3, "Confections")
                );

            modelBuilder.Entity<AccountMember>().HasKey(a => a.MemberID);
            modelBuilder.Entity<AccountMember>()
                .HasData(
                    new AccountMember("PS001", "123", "HungHV", "hoanghung250604@gmail.com", 1)
                );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey("CategoryID");

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product(1, "Chai", 3, 12, 18),
                    new Product(2, "Chang", 1, 23, 19),
                    new Product(3, "Oto", 2, 12, 30),
                    new Product(4, "Aniseed", 2, 12, 27)
                );
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            return config["ConnectionStrings:MyStockDB"];
        }
    }
}
