using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.API.Domain.Entities;

namespace Music.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.Aggregates.Music> Musics { get; set; }
        public DbSet<Producer> ProducerSnapshots { get; set; }
        public DbSet<Album> AlbumSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producer>().ToTable("ProducerSnapshots");
        }
    }
}