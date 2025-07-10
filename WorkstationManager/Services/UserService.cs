using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkstationManager.Data;
using WorkstationManager.Models;

namespace WorkstationManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Role?> GetUserRoleAsync(string roleName)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}
