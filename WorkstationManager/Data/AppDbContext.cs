using Microsoft.EntityFrameworkCore;
using System;
using WorkstationManager.Models;
using static BCrypt.Net.BCrypt;

namespace WorkstationManager.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<WorkPosition> WorkPositions { get; set; } = null!;
        public DbSet<UserWorkPosition> UserWorkPositions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                "server=localhost;database=workstation_db;user=root;password=password123;",
                new MySqlServerVersion(new Version(8, 0, 42))
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "User" }
            );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "dario123", Password = HashPassword("pw"), RoleId = 1 },
                new User { Id = 2, Username = "pero3", Password = HashPassword("pw"), RoleId = 2 }
            );
        }

    }
}
