using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkstationManager.Services;
using WorkstationManager.ViewModels;

public static class AppHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IAdminService, AdminService>();

                services.AddScoped<MainWindowViewModel>();
                services.AddScoped<AdminViewModel>();
                services.AddScoped<UserViewModel>();

            });
}
