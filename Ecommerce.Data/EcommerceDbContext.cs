using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EcommerceAppDbContext : DbContext
    {
        public EcommerceAppDbContext()
        {
        }
        public EcommerceAppDbContext(DbContextOptions<EcommerceAppDbContext> options) : base(options)
        {
        }
        public DbSet<ProductDto> Products { get; set; }

        public DbSet<OrderHeaderDto> OrderHeaders { get; set; }

        public DbSet<OrderLineDto> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDto>().ToTable("Products").HasKey(s => s.ProductId);
            modelBuilder.Entity<OrderHeaderDto>().ToTable("OrderHeaders").HasKey(s => s.OrderId);
            modelBuilder.Entity<OrderLineDto>().ToTable("OrderLines").HasKey(s => s.OrderLineId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True\r\n");
            }
        }
    }
}

