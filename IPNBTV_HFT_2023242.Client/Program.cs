using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections;
using System.Linq;

namespace IPNBTV_HFT_2023242.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RentalShopDbContext ctx = new RentalShopDbContext();

            IRepository<RentalEvent> eventRepo = new RentalEventRepository(ctx);
            IRepository<Car> carRepo = new CarRepository(ctx);

            IRentalEventLogic eventLogic = new RentalEventLogic(eventRepo, carRepo);

            DateTime startDate = new DateTime(2020, 05, 05);
            DateTime endDate = DateTime.Now;

            var mostRentedCar = eventLogic.OrderByMostRentals(startDate, endDate);

            var customerWithAutomatic = eventLogic.GetCustomersWithAutomaticTransmission();

            var mostRentedCarInRentalShop = eventLogic.GetMostRentedCarsInRentalShop(0);

            ;
        }
    }
}
