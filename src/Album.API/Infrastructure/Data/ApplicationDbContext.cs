using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Snapshots;
using Microsoft.EntityFrameworkCore;

namespace Album.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Aggregates.Album>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2')");

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Domain.Aggregates.Album> Albums { get; set; }
        public DbSet<ProducerSnap> ProducerSnapshots { get; set; }
    }
}