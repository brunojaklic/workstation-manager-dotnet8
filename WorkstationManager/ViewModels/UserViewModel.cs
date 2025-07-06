using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkstationManager.Data;
using WorkstationManager.Models;

namespace WorkstationManager.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty] private string firstName = "";
        [ObservableProperty] private string lastName = "";
        [ObservableProperty] private string workstation = "";
        [ObservableProperty] private string productName = "";
        [ObservableProperty] private string assignmentDate = "";

        public UserViewModel(User user)
        {
            FirstName = user.FirstName ?? "";
            LastName = user.LastName ?? "";

            LoadLatestWorkstation(user.Id);
        }

        private async void LoadLatestWorkstation(int userId)
        {
            using var db = new AppDbContext();
            var latestAssignment = await db.UserWorkPositions
                .Include(wp => wp.WorkPosition)
                .Where(wp => wp.UserId == userId)
                .OrderByDescending(wp => wp.WorkDate)
                .FirstOrDefaultAsync();

            if (latestAssignment != null)
            {
                Workstation = latestAssignment.WorkPosition.WorkPositionName;
                ProductName = latestAssignment.ProductName;
                AssignmentDate = latestAssignment.WorkDate.ToString("yyyy-MM-dd");
            }
            else
            {
                Workstation = "No workstation assigned";
                ProductName = "-";
                AssignmentDate = "-";
            }
        }
    }
}
