using Microsoft.EntityFrameworkCore;
using IPNBTV_HFT_2023242.Models;
using System;

namespace IPNBTV_HFT_2023242.Repository
{
    public class RentalShopDbContext : DbContext
    {
        public DbSet<RentalShop> RentalShops { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }

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

        
    }
}
