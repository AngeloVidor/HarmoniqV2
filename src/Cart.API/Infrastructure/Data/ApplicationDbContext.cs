using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Snapshots;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Aggregates.Cart>(e =>
            {
                e.Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Domain.Aggregates.CartItem>(e =>
            {
                e.Property(p => p.Price)

                .HasColumnType("decimal(18,2)");
            });


        }

        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Domain.Aggregates.Cart> Carts { get; set; }
        public DbSet<Domain.Aggregates.CartItem> CartItems { get; set; }
    }
}