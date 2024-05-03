using Microsoft.EntityFrameworkCore;
using IPNBTV_HFT_2023242.Models;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IPNBTV_HFT_2023242.Repository
{
    public class RentalShopDbContext : DbContext
    {
        public DbSet<RentalShop> RentalShops { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalEvent> RentalEvents { get; set; }

        public RentalShopDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RentalShop.mdf;Integrated Security=True;MultipleActiveResultSets=True";
                optionsBuilder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasOne(p => p.RentalShop)
                                      .WithMany(p => p.Cars)
                                      .HasForeignKey(p => p.RentalShopId);

            modelBuilder.Entity<RentalEvent>().HasOne(p => p.Car)
                                              .WithMany(p => p.RentalEvents)
                                              .HasForeignKey(p => p.CarId);

            var rentalshops = JsonConvert.DeserializeObject<List<RentalShop>>(File.ReadAllText("rentalshops.json"));
            var cars = JsonConvert.DeserializeObject<List<Car>>(File.ReadAllText("cars.json"));
            var rentalevents = JsonConvert.DeserializeObject<List<RentalEvent>>(File.ReadAllText("rentalevents.json"));

            modelBuilder.Entity<RentalShop>().HasData(rentalshops);
            modelBuilder.Entity<Car>().HasData(cars);
            modelBuilder.Entity<RentalEvent>().HasData(rentalevents);
        }
    }
}
