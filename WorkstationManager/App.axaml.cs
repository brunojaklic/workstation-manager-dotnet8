using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkstationManager.Data;
using WorkstationManager.Services;
using WorkstationManager.ViewModels;
using WorkstationManager.Views;

namespace WorkstationManager
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainWindowViewModel,
                };
                desktop.MainWindow.Show();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(
                    "server=localhost;port=3306;database=workstation_db;user=root;password=password123;",
                    new MySqlServerVersion(new System.Version(8, 0, 42))
                );
            });
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAdminService, AdminService>();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AdminViewModel>();
        }
    }
}
