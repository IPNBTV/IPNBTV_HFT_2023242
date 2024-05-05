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
            IRepository<RentalShop> shopRepo = new RentalShopRepository(ctx);

            IRentalEventLogic eventLogic = new RentalEventLogic(eventRepo, carRepo);
            ICarLogic carLogic = new CarLogic(carRepo, eventRepo);
            IRentalShopLogic shopLogic = new RentalShopLogic(shopRepo, carRepo);

            DateTime startDate = new DateTime(2020, 05, 05);
            DateTime endDate = DateTime.Now;

            var mostRentedCar = eventLogic.OrderByMostRentals(startDate, endDate);

            var customerWithAutomatic = eventLogic.GetCustomersWithAutomaticTransmission();

            var mostRentedCarInRentalShop = carLogic.GetMostRentedCarsInRentalShop(1);

            var numberOfRentalEventsForCarInRentalShop = carLogic.GetNumberOfRentalEventsForCarInShop(4, 1);

            var rentalShopsWithCarColor = shopLogic.GetRentalShopsWithCarColor("silver");

            var newEvent = new RentalEvent
            {
                Id = 13,
                CarId = 4,
                RentalDate = DateTime.Parse("2012. 05. 01. 10:00:00"),
                ReturnDate = DateTime.Parse("2012. 12. 05. 15:00:00"),
                CustomerName = "Dávid Máté"
            };

            eventRepo.Create(newEvent);

            var e = eventRepo.Read(13);

            ;
        }
    }
}
