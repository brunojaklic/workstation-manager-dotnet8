using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkstationManager.Models;
using WorkstationManager.Services;

namespace WorkstationManager.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        private readonly User currentUser;
        private readonly IRelayCommand signOutCommand;
        private readonly IUserService userService;
        private readonly IAdminService AdminService;

        public AdminViewModel(
            User user,
            IRelayCommand signOutCommand,
            IUserService userService,
            IAdminService AdminService)
        {
            currentUser = user;
            this.signOutCommand = signOutCommand;
            this.userService = userService;
            this.AdminService = AdminService;

            Users = new ObservableCollection<User>();
            WorkPositions = new ObservableCollection<WorkPosition>();

            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            ChangeAssignmentCommand = new AsyncRelayCommand(ChangeAssignmentAsync);
            CreateUserCommand = new AsyncRelayCommand(CreateUserAsync);

            _ = LoadDataAsync();
        }

        public IRelayCommand SignOutCommand => signOutCommand;

        [ObservableProperty] private ObservableCollection<User> users;
        [ObservableProperty] private User? selectedUser;
        [ObservableProperty] private ObservableCollection<WorkPosition> workPositions;
        [ObservableProperty] private WorkPosition? selectedWorkPosition;
        [ObservableProperty] private string newUsername = "";
        [ObservableProperty] private string newFirstName = "";
        [ObservableProperty] private string newLastName = "";
        [ObservableProperty] private string newPassword = "";
        [ObservableProperty] private string creationErrorMessage = "";
        [ObservableProperty] private string creationSuccessMessage = "";
        [ObservableProperty] private string productName = "";
        [ObservableProperty] private WorkPosition? newUserWorkPosition;
        [ObservableProperty] private string newUserProductName = "";

        private string selectedUserCurrentAssignmentDate = "-";

        public string SelectedUserCurrentAssignmentDate
        {
            get => selectedUserCurrentAssignmentDate;
            private set => SetProperty(ref selectedUserCurrentAssignmentDate, value);
        }

        public IAsyncRelayCommand LoadDataCommand { get; }
        public IAsyncRelayCommand ChangeAssignmentCommand { get; }
        public IAsyncRelayCommand CreateUserCommand { get; }

        partial void OnSelectedUserChanged(User? oldValue, User? newValue)
        {
            _ = OnSelectedUserChangedAsync(newValue);
        }

        private async Task OnSelectedUserChangedAsync(User? newValue)
        {
            if (newValue == null)
            {
                SelectedWorkPosition = null;
                ProductName = "";
                return;
            }

            var assignment = await AdminService.GetLatestAssignmentAsync(newValue.Id);

            SelectedWorkPosition = assignment != null
                ? WorkPositions.FirstOrDefault(wp => wp.Id == assignment.WorkPositionId)
                : null;

            ProductName = assignment?.ProductName ?? "";

            OnPropertyChanged(nameof(SelectedUserCurrentAssignmentDate));
        }

        private async Task UpdateSelectedUserAssignmentDateAsync(int userId)
        {
            if (userId == 0)
            {
                SelectedUserCurrentAssignmentDate = "-";
                return;
            }

            var assignment = await AdminService.GetLatestAssignmentAsync(userId);
            SelectedUserCurrentAssignmentDate = assignment?.WorkDate.ToString("yyyy-MM-dd") ?? "-";
        }

        private async Task LoadDataAsync()
        {
            var users = await userService.GetAllUsersAsync();
            var workPositions = await AdminService.GetAllWorkstationsAsync();

            Users.Clear();
            foreach (var user in users)
                Users.Add(user);

            WorkPositions.Clear();
            foreach (var wp in workPositions)
                WorkPositions.Add(wp);

            if (Users.Any() && SelectedUser == null)
                SelectedUser = Users[0];
        }

        private async Task ChangeAssignmentAsync()
        {
            if (SelectedUser == null || SelectedWorkPosition == null)
                return;

            var newAssignment = new UserWorkPosition
            {
                UserId = SelectedUser.Id,
                WorkPositionId = SelectedWorkPosition.Id,
                ProductName = ProductName,
                WorkDate = DateTime.Now
            };

            await AdminService.AssignUserAsync(newAssignment);

            await UpdateSelectedUserAssignmentDateAsync(SelectedUser.Id);

            var selectedUserId = SelectedUser.Id;
            await LoadDataAsync();
            SelectedUser = Users.FirstOrDefault(u => u.Id == selectedUserId);
        }

        private async Task CreateUserAsync()
        {
            CreationErrorMessage = "";
            CreationSuccessMessage = "";

            if (string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
            {
                CreationErrorMessage = "Username and Password are required.";
                return;
            }

            if (NewUsername.Length > 20 || NewUsername.Length < 3)
            {
                CreationErrorMessage = "Username must be between 3 and 20 characters long.";
                return;
            }

            if (!Regex.IsMatch(NewUsername, @"^[a-zA-Z0-9]+$"))
            {
                CreationErrorMessage = "Username can only contain alphanumeric characters.";
                return;
            }

            if (NewUserWorkPosition == null)
            {
                CreationErrorMessage = "Please select a work position for the new user.";
                return;
            }

            if (await userService.UsernameExistsAsync(NewUsername))
            {
                CreationErrorMessage = "Username already exists.";
                return;
            }

            var defaultRole = await userService.GetUserRoleAsync("User");
            if (defaultRole == null)
            {
                CreationErrorMessage = "Default user role not found in the database.";
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);

            var newUser = new User
            {
                Username = NewUsername,
                FirstName = NewFirstName,
                LastName = NewLastName,
                Password = hashedPassword,
                RoleId = defaultRole.Id,
            };

            var createdUser = await userService.CreateUserAsync(newUser);

            var assignment = new UserWorkPosition
            {
                UserId = createdUser.Id,
                WorkPositionId = NewUserWorkPosition.Id,
                ProductName = NewUserProductName,
                WorkDate = DateTime.Now
            };

            await AdminService.AssignUserAsync(assignment);

            CreationSuccessMessage = "User created successfully!";
            NewUsername = "";
            NewFirstName = "";
            NewLastName = "";
            NewPassword = "";
            NewUserWorkPosition = null;
            NewUserProductName = "";

            await LoadDataAsync();
            SelectedUser = Users.FirstOrDefault(u => u.Username == createdUser.Username);
        }

    }
}
