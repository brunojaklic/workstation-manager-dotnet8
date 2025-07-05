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
                new Role { Id = 1, RoleName = "Admin", RoleDescription = "Administrator with full access" },
                new Role { Id = 2, RoleName = "User", RoleDescription = "Standard user with limited access" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Password = HashPassword("123"),
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Username = "janesmith",
                    Password = HashPassword("123"),
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Username = "rjohnson",
                    Password = HashPassword("123"),
                    RoleId = 2
                },
                new User
                {
                    Id = 4,
                    FirstName = "Emily",
                    LastName = "Williams",
                    Username = "ewilliams",
                    Password = HashPassword("123"),
                    RoleId = 2
                }
            );

            modelBuilder.Entity<WorkPosition>().HasData(
                new WorkPosition
                {
                    Id = 1,
                    WorkPositionName = "Developer",
                    WorkPositionDescription = "Software development position"
                },
                new WorkPosition
                {
                    Id = 2,
                    WorkPositionName = "Designer",
                    WorkPositionDescription = "UI/UX design position"
                },
                new WorkPosition
                {
                    Id = 3,
                    WorkPositionName = "Tester",
                    WorkPositionDescription = "Quality assurance position"
                },
                new WorkPosition
                {
                    Id = 4,
                    WorkPositionName = "DevOps",
                    WorkPositionDescription = "Infrastructure and deployment position"
                }
            );

            modelBuilder.Entity<UserWorkPosition>().HasData(
                new UserWorkPosition
                {
                    Id = 1,
                    UserId = 1,
                    WorkPositionId = 1,
                    ProductName = "ERP System",
                    WorkDate = new DateTime(2023, 1, 15)
                },
                new UserWorkPosition
                {
                    Id = 2,
                    UserId = 1,
                    WorkPositionId = 4,
                    ProductName = "CRM Platform",
                    WorkDate = new DateTime(2023, 2, 20)
                },
                new UserWorkPosition
                {
                    Id = 3,
                    UserId = 2,
                    WorkPositionId = 2,
                    ProductName = "Mobile App",
                    WorkDate = new DateTime(2023, 3, 10)
                },
                new UserWorkPosition
                {
                    Id = 4,
                    UserId = 3,
                    WorkPositionId = 1,
                    ProductName = "Data Analytics Tool",
                    WorkDate = new DateTime(2023, 4, 5)
                },
                new UserWorkPosition
                {
                    Id = 5,
                    UserId = 4,
                    WorkPositionId = 3,
                    ProductName = "E-commerce Website",
                    WorkDate = new DateTime(2023, 5, 12)
                }
            );
        }

    }
}
