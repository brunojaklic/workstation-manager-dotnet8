using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkstationManager.Data;
using WorkstationManager.Models;

namespace WorkstationManager.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        private readonly User currentUser;
        private readonly IRelayCommand signOutCommand;

        public AdminViewModel(User user, IRelayCommand signOutCommand)
        {
            currentUser = user;
            this.signOutCommand = signOutCommand;

            Users = new ObservableCollection<User>();
            WorkPositions = new ObservableCollection<WorkPosition>();

            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            ChangeAssignmentCommand = new AsyncRelayCommand(ChangeAssignmentAsync);
            CreateUserCommand = new AsyncRelayCommand(CreateUserAsync);

            _ = LoadDataAsync();
        }

        public IRelayCommand SignOutCommand => signOutCommand;

        [ObservableProperty]
        private ObservableCollection<User> users;

        [ObservableProperty]
        private User? selectedUser;

        [ObservableProperty]
        private ObservableCollection<WorkPosition> workPositions;

        [ObservableProperty]
        private WorkPosition? selectedWorkPosition;

        [ObservableProperty]
        private string newUsername = "";

        [ObservableProperty]
        private string newFirstName = "";

        [ObservableProperty]
        private string newLastName = "";

        [ObservableProperty]
        private string newPassword = "";

        [ObservableProperty]
        private string creationErrorMessage = "";

        [ObservableProperty]
        private string creationSuccessMessage = "";

        [ObservableProperty]
        private string productName = "";

        [ObservableProperty]
        private WorkPosition? newUserWorkPosition;

        [ObservableProperty]
        private string newUserProductName = "";

        public string SelectedUserCurrentWorkPosition
        {
            get
            {
                if (SelectedUser == null) return "-";
                using var db = new AppDbContext();
                var assignment = db.UserWorkPositions
                    .Include(uwp => uwp.WorkPosition)
                    .Where(uwp => uwp.UserId == SelectedUser.Id)
                    .OrderByDescending(uwp => uwp.WorkDate)
                    .FirstOrDefault();

                return assignment?.WorkPosition.WorkPositionName ?? "No work position assigned";
            }
        }

        public string SelectedUserCurrentAssignmentDate
        {
            get
            {
                if (SelectedUser == null) return "-";
                using var db = new AppDbContext();
                var assignment = db.UserWorkPositions
                    .Where(uwp => uwp.UserId == SelectedUser.Id)
                    .OrderByDescending(uwp => uwp.WorkDate)
                    .FirstOrDefault();

                return assignment?.WorkDate.ToString("yyyy-MM-dd") ?? "-";
            }
        }

        public string SelectedUserCurrentProductName
        {
            get
            {
                if (SelectedUser == null) return "-";
                using var db = new AppDbContext();
                var assignment = db.UserWorkPositions
                    .Where(uwp => uwp.UserId == SelectedUser.Id)
                    .OrderByDescending(uwp => uwp.WorkDate)
                    .FirstOrDefault();

                return assignment?.ProductName ?? "-";
            }
        }

        public IAsyncRelayCommand LoadDataCommand { get; }
        public IAsyncRelayCommand ChangeAssignmentCommand { get; }
        public IAsyncRelayCommand CreateUserCommand { get; }

        partial void OnSelectedUserChanged(User? oldValue, User? newValue)
        {
            OnPropertyChanged(nameof(SelectedUserCurrentWorkPosition));
            OnPropertyChanged(nameof(SelectedUserCurrentAssignmentDate));
            OnPropertyChanged(nameof(SelectedUserCurrentProductName));

            using var db = new AppDbContext();

            int? selectedUserId = newValue?.Id;
            if (selectedUserId == null)
            {
                SelectedWorkPosition = null;
                return;
            }

            var assignment = db.UserWorkPositions
                .Where(uwp => uwp.UserId == selectedUserId.Value)
                .OrderByDescending(uwp => uwp.WorkDate)
                .FirstOrDefault();

            SelectedWorkPosition = assignment != null
                ? WorkPositions.FirstOrDefault(wp => wp.Id == assignment.WorkPositionId)
                : null;
        }

        private async Task LoadDataAsync()
        {
            using var db = new AppDbContext();

            var users = await db.Users.Include(u => u.Role).ToListAsync();
            var workPositions = await db.WorkPositions.ToListAsync();

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

            using var db = new AppDbContext();

            var newAssignment = new UserWorkPosition
            {
                UserId = SelectedUser.Id,
                WorkPositionId = SelectedWorkPosition.Id,
                ProductName = ProductName,
                WorkDate = DateTime.Now
            };

            db.UserWorkPositions.Add(newAssignment);
            await db.SaveChangesAsync();

            OnPropertyChanged(nameof(SelectedUserCurrentWorkPosition));
            OnPropertyChanged(nameof(SelectedUserCurrentAssignmentDate));
            OnPropertyChanged(nameof(SelectedUserCurrentProductName));

            var selectedUserId = SelectedUser.Id;
            await LoadDataAsync();

            SelectedUser = Users.FirstOrDefault(u => u.Id == selectedUserId);

            ProductName = "";
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

            using var db = new AppDbContext();

            var exists = await db.Users.AnyAsync(u => u.Username == NewUsername);
            if (exists)
            {
                CreationErrorMessage = "Username already exists.";
                return;
            }

            var userRole = await db.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
            if (userRole == null)
            {
                CreationErrorMessage = "User role not found.";
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);

            var newUser = new User
            {
                Username = NewUsername,
                FirstName = NewFirstName,
                LastName = NewLastName,
                Password = hashedPassword,
                RoleId = userRole.Id
            };

            db.Users.Add(newUser);
            await db.SaveChangesAsync();

            var assignment = new UserWorkPosition
            {
                UserId = newUser.Id,
                WorkPositionId = NewUserWorkPosition.Id,
                ProductName = NewUserProductName,
                WorkDate = DateTime.Now
            };

            db.UserWorkPositions.Add(assignment);
            await db.SaveChangesAsync();

            CreationSuccessMessage = "User created successfully!";

            NewUsername = "";
            NewFirstName = "";
            NewLastName = "";
            NewPassword = "";
            NewUserWorkPosition = null;
            NewUserProductName = "";

            await LoadDataAsync();

            SelectedUser = Users.FirstOrDefault(u => u.Id == newUser.Id);
        }
    }
}
