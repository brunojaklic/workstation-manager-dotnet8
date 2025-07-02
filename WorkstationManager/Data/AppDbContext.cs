using Microsoft.EntityFrameworkCore;
using System;
using WorkstationManager.Models;

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
                "server=localhost;database=workstation_db;user=root;password=yourpassword;",
                new MySqlServerVersion(new Version(8, 0, 42))
            );
        }

    }
}
