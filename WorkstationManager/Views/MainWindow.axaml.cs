using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace WorkstationManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SignInButton.Click += SignInButton_Click;
        }

        private void SignInButton_Click(object? sender, RoutedEventArgs e)
        {
            
        }
    }
}