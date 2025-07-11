using System.Collections.Generic;
using System.Threading.Tasks;
using WorkstationManager.Models;

namespace WorkstationManager.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<User?> CreateUserAsync(User user);
        Task<Role?> GetUserRoleAsync(string roleName);
    }
}
