using System.Collections.Generic;
using System.Threading.Tasks;
using WorkstationManager.Models;

public interface IAdminService
{
    Task<List<WorkPosition>> GetAllWorkstationsAsync();
    Task<UserWorkPosition?> GetLatestAssignmentAsync(int userId);
    Task AssignUserAsync(UserWorkPosition assignment);
}