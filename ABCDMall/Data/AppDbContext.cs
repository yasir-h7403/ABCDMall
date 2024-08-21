using ABCDMall.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ABCDMall.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shops> Shops { get; set; }
        public DbSet<FoodCourt> FoodCourts { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shops>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Shops>()
                .Property(s => s.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<FoodCourt>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FoodCourt>()
                .Property(f => f.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Movie>()
                .Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>()
                .Property(m => m.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Gallery>()
                .Property(g => g.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Gallery>()
                .Property(g => g.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            // Configuring Feedback model
            modelBuilder.Entity<Feedback>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // Default value for CreatedAt

            modelBuilder.Entity<Feedback>()
                .Property(f => f.UpdatedAt)
                .HasDefaultValueSql("GETDATE()"); // Default value for UpdatedAt

            modelBuilder.Entity<Ticket>()
                .Property(t => t.BookingTimeCreatedAt)
                .HasDefaultValueSql("GETDATE()"); // Default value for CreatedAt

            modelBuilder.Entity<Ticket>()
                .Property(t => t.BookingTimeUpdatedAt)
                .HasDefaultValueSql("GETDATE()"); // Default value for UpdatedAt

            // Add configurations for other models if needed
        }
    }
    }
