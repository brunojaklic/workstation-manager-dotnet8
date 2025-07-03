using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

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
            
        }
    }
}
