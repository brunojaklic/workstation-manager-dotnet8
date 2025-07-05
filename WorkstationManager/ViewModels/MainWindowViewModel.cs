using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkstationManager.Data;
using WorkstationManager.ViewModels;
using static BCrypt.Net.BCrypt;

namespace WorkstationManager.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty] private string username = "";
        [ObservableProperty] private string password = "";
        [ObservableProperty] private string errorMessage = "";
        [ObservableProperty] private object? currentViewModel;
        [ObservableProperty] private bool isLoginVisible = true;


        public MainWindowViewModel()
        {
            CurrentViewModel = this;
        }

        [RelayCommand]
        private async Task SignIn()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Both username and password are required";
                return;
            }

            try
            {
                using var db = new AppDbContext();

                var user = await db.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == Username);

                if (user != null && Verify(Password, user.Password))
                {
                    ErrorMessage = "";
                    Password = "";
                    IsLoginVisible = false;

                    if (user.Role.RoleName == "Admin")
                    {
                        CurrentViewModel = new AdminViewModel(user);
                    }
                    else if (user.Role.RoleName == "User")
                    {
                        CurrentViewModel = new UserViewModel(user);
                    }
                    else
                    {
                        ErrorMessage = "Unknown user role";
                    }
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                    Password = "";
                }
            }
            catch (System.Exception e)
            {
                ErrorMessage = "Login error: " + e.Message;
            }
        }
    }
}
