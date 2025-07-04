using Avalonia.Controls;
using WorkstationManager.ViewModels;

namespace WorkstationManager.Views
{
    internal class UserWindow : Window
    {
        public UserViewModel DataContext { get; set; }
    }
}