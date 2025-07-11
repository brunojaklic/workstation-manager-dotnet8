using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace WorkstationManager.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;database=workstation_db;user=root;password=root;",
                new MySqlServerVersion(new Version(8, 0, 42))
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
