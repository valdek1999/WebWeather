using Microsoft.EntityFrameworkCore;
using WebWeather.DataAccess.Models;

namespace WebWeather.DataAccess
{
    public class DataWeatherContext: DbContext
    {
        public DbSet<Weather> Weather { get; set; } = null!;
        public DataWeatherContext(DbContextOptions<DataWeatherContext> options)
            : base(options)
        {
            Database.EnsureCreated();// создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weather>();//.HasKey("Id");
        }
    }
}
