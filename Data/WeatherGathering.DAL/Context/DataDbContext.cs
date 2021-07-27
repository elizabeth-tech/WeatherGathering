using Microsoft.EntityFrameworkCore;
using System;

namespace WeatherGathering.DAL.Context
{
    public class DataDbContext : DbContext
    {
        public DbSet<DataValue> Values { get; set; }

        public DbSet<DataSource> Sources { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne(v => v.Source) // делаем отношение один ко многим
                .OnDelete(DeleteBehavior.Cascade); // включаем каскадное удаление

            //model.Entity<DataSource>()
            //    .Property(source => source.Name)
            //    .IsRequired();
        }
    }
}
