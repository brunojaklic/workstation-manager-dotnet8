using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WorkstationManager.Models;
using WorkstationManager.Services;

namespace WorkstationManager.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly IAdminService workstationService;

        [ObservableProperty] private string firstName = "";
        [ObservableProperty] private string lastName = "";
        [ObservableProperty] private string workstation = "";
        [ObservableProperty] private string productName = "";
        [ObservableProperty] private string assignmentDate = "";

        public IRelayCommand SignOutCommand { get; }

        public UserViewModel(User user, IRelayCommand signOutCommand, IAdminService workstationService)
        {
            FirstName = user.FirstName ?? "";
            LastName = user.LastName ?? "";
            SignOutCommand = signOutCommand;
            this.workstationService = workstationService;

            _ = LoadLatestWorkstationAsync(user.Id);
        }

        private async Task LoadLatestWorkstationAsync(int userId)
        {
            var latestAssignment = await workstationService.GetLatestAssignmentAsync(userId);

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
