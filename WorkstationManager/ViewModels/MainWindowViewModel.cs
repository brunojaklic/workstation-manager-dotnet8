using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WorkstationManager.Services;

namespace WorkstationManager.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IUserService userService;
        private readonly IAdminService workstationService;

        public MainWindowViewModel(IUserService userService, IAdminService workstationService)
        {
            this.userService = userService;
            this.workstationService = workstationService;
        }

        [ObservableProperty] private string username = "";
        [ObservableProperty] private string password = "";
        [ObservableProperty] private string errorMessage = "";
        [ObservableProperty] private object? currentViewModel;
        [ObservableProperty] private bool isLoginVisible = true;

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
                var user = await userService.GetByUsernameAsync(Username);

                if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    ErrorMessage = "";
                    Password = "";
                    IsLoginVisible = false;

                    if (user.Role.RoleName == "Admin")
                    {
                        CurrentViewModel = new AdminViewModel(user, SignOutCommand, userService, workstationService);
                    }
                    else if (user.Role.RoleName == "User")
                    {
                        CurrentViewModel = new UserViewModel(user, SignOutCommand, workstationService);
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

        [RelayCommand]
        private void SignOut()
        {
            CurrentViewModel = null;
            Username = "";
            Password = "";
            ErrorMessage = "";
            IsLoginVisible = true;
        }
    }
}
