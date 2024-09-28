using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotPlateRestaurant.DAL
{
    public class DBContexto : DbContext
    {
        public DbSet<foodTable> foodTable { get; set; }
        public DbSet<categoryTable> categoryTable { get; set; }
        public DbSet<orderTable> orderTable { get; set; }

        public DbSet<userTable> userTable { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<FoodImages> foodimages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=hotplaterestaurant;User=root;Password=password;",
                new MySqlServerVersion(new Version(8, 0, 0)));
        }
    }
}
