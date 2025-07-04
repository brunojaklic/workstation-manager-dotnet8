using Avalonia.Controls;
using WorkstationManager.ViewModels;

namespace WorkstationManager.Views
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}