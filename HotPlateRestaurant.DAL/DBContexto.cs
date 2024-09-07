using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotPlateRestaurant.DAL
{
    public class DBContexto : DbContext
    {
        public DbSet<foodTable> Foods { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=hotplaterestaurant;User=root;Password=password;",
                new MySqlServerVersion(new Version(8, 0, 0)));
        }
    }
}
