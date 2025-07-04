using Avalonia.Controls;
using WorkstationManager.ViewModels;

namespace WorkstationManager.Views
{
    internal class AdminWindow : Window
    {
        public AdminViewModel DataContext { get; set; }
    }
}