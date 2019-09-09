using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(InitialDataSeed.Players);
            modelBuilder.Entity<Treasure>().HasData(InitialDataSeed.Treasures);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Treasure> Treasures { get; set; }
    }
}
