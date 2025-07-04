using Avalonia.Controls;
using WorkstationManager.ViewModels;

namespace WorkstationManager.Views
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}