using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherGathering.API.Data;
using WeatherGathering.DAL.Context;
using WeatherGathering.DAL.Repositories;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.API
{
    public record Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(
                opt => opt
                .UseSqlServer(configuration
                .GetConnectionString("Data"), o => o.MigrationsAssembly("WeatherGathering.DAL.SqlServer")));

            services.AddTransient<DataDbInitializer>();

            // Контейнер сервисов автоматически подставляет репозиторий
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(INamedEntityRepository<>), typeof(DbNamedEntityRepository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherGathering.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDbInitializer dbInitializer)
        {
            dbInitializer.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherGathering.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
