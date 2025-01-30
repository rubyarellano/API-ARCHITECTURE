using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MovieRental.Data
{
    public class MovieRentalDbContext : DbContext
    {
        public MovieRentalDbContext(DbContextOptions<MovieRentalDbContext> options) : base(options) { }

        public DbSet<Customers>? Customer { get; set; }

        public DbSet<Movie>? Movies{ get; set; }

        public DbSet<RentalDetail>? RentalDetails { get; set; }

        public DbSet<RentalHeader>? RentalHeader { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                         .HasKey(c => c.CustomerName);
            modelBuilder.Entity<Movie>()
                       .HasKey(c => c.MovieId);
            modelBuilder.Entity<RentalDetail>()
                       .HasKey(c => c.MovieId);
            modelBuilder.Entity<RentalHeader>()
                       .HasKey(c => c.RentalId);




        }

    }
}
