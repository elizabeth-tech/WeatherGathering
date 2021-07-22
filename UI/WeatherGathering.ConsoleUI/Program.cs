using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WeatherGathering.ConsoleUI
{
    class Program
    {
        private static IHost hosting;

        public static IHost Hosting => hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        // Создаем хост приложения и конфигурируем сервисы
        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        // Регистрируем все сервисы
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //services.AddHttpClient<MetaWeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeather"]))
            //    // настройки для клиента
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy());
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            Console.WriteLine("Completed!");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}
