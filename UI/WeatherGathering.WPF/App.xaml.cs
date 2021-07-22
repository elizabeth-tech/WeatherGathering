using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using WeatherGathering.WPF.ViewModels;
using WeatherGathering.WPF.Views.Windows;

namespace WeatherGathering.WPF
{
    public partial class App
    {
        private static IHost hosting;

        public static IHost Hosting => hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        #region Свойства окон
        public static Window WindowActive => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window WindowFocused => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window WindowCurrent => WindowFocused ?? WindowActive;
        # endregion Свойства окон

        // Создаем хост приложения и конфигурируем сервисы
        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddScoped<MainWindowViewModel>();
        }

        // Запуск хостинга
        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Hosting;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(true);
            //Services.GetRequiredService<MainWindow>().Show();

        }

        // Остановка хостинга
        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Hosting;
            base.OnExit(e);
            await host.StopAsync().ConfigureAwait(false);
        }
    }
}
