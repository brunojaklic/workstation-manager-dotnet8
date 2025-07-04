using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using WorkstationManager.Data;

namespace WorkstationManager.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username = "";

        [ObservableProperty]
        private string password = "";

        [RelayCommand]
        private void SignIn()
        {
            using var db = new AppDbContext();

            var user = db.Users
                .FirstOrDefault(u => u.Username == Username && u.PasswordHash == Password);

            if (user != null)
            {

            }
            else
            {

            }
        }

    }
}
