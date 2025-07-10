using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkstationManager.Data;
using WorkstationManager.Models;

namespace WorkstationManager.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _dbContext;

        public AdminService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkPosition>> GetAllWorkstationsAsync()
        {
            return await _dbContext.WorkPositions.ToListAsync();
        }

        public async Task<UserWorkPosition?> GetLatestAssignmentAsync(int userId)
        {
            return await _dbContext.UserWorkPositions
                .Include(wp => wp.WorkPosition)
                .Where(wp => wp.UserId == userId)
                .OrderByDescending(wp => wp.WorkDate)
                .FirstOrDefaultAsync();
        }

        public async Task AssignUserAsync(UserWorkPosition assignment)
        {
            _dbContext.UserWorkPositions.Add(assignment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
