using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherGathering.DAL;
using WeatherGathering.Interfaces.Base.Repositories;
using WeatherGathering.WebAPIClients.Repositories;

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
            //hhtps://localhost:44301/swagger/index.html
            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(
                client =>
                {
                    client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/");
                });
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            var data_sources = Services.GetRequiredService<IRepository<DataSource>>();

            var start_count = await data_sources.GetCount();

            Console.WriteLine($"\n>>>   Привет. Я тестовое консольное приложение.\n");

            Console.WriteLine($"\n>>>   Элементов в репозитории в начале работы: {start_count}\n");

            var sources = await data_sources.GetAll();

            //var sources = await data_sources.GetSkip(3, 5);

            Console.WriteLine($"\n>>>   Вывожу все элементы репозитория...\n");
            foreach (var source in sources)
            {
                Console.WriteLine($"{source.Id}-{source.Name}");
            }
            Console.WriteLine();

            //var page = await data_sources.GetPage(4, 3);

            //var added_source = await data_sources.Add(
            //    new DataSource
            //    {
            //        Name=$"Source {DateTime.Now:HH-mm-ss}",
            //        Description= $"New source {DateTime.Now:HH-mm-ss}"
            //    });

            //var edit_item = await data_sources.Update(
            //    new DataSource
            //    {
            //        Id = 1,
            //        Name = $"Edited item",
            //        Description = $"Edited descr"
            //    });

            //var first = (await data_sources.GetSkip(0, 2).ConfigureAwait(false)).ToArray();

            //var deleted = await data_sources.DeleteById(first[0].Id);
            //var _deleted = await data_sources.Delete(first[1]);

            var end_count = await data_sources.GetCount();

            Console.WriteLine($"\n>>>   Элементов в репозитории в конце работы: {end_count}\n");


            Console.WriteLine("Все действия отработаны успешно!");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}
