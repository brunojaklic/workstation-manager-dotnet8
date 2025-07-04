using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkstationManager.Data;
using Avalonia;
using WorkstationManager.Views;

namespace WorkstationManager.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string errorMessage = "";

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

                if (user != null && VerifyPassword(Password, user.PasswordHash))
                {
                    ErrorMessage = "";
                    Password = "";

                    var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
                    var mainWindow = lifetime?.MainWindow;

                    if (user.Role.RoleName == "Admin")
                    {
                        var adminWindow = new AdminWindow
                        {
                            DataContext = new AdminViewModel(user)
                        };
                        await adminWindow.ShowDialog(mainWindow);
                    }
                    else if (user.Role.RoleName == "User")
                    {
                        var userWindow = new UserWindow
                        {
                            DataContext = new UserViewModel(user)
                        };
                        await userWindow.ShowDialog(mainWindow);
                    }
                    else
                    {
                        ErrorMessage = "Unknown user role";
                        return;
                    }

                    var loginWindow = lifetime?.Windows.FirstOrDefault(w => w.DataContext == this);
                    loginWindow?.Close();
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                    Password = "";
                }
            }
            catch (Exception e)
            {
                ErrorMessage = "Login error: " + e.Message;
            }
        }


        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }
    }
}
