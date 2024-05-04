using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Logic
{
    public class RentalEventLogic : IRentalEventLogic
    {
        IRepository<RentalEvent> eventRepo;
        IRepository<Car> carRepo;

        public RentalEventLogic(IRepository<RentalEvent> eventRepo, IRepository<Car> carRepo)
        {
            this.eventRepo = eventRepo;
            this.carRepo = carRepo;
        }

        public void Create(RentalEvent item)
        {
            if (item.CustomerName.Length < 3)
            {
                throw new ArgumentException("Invalid Id");
            }
            eventRepo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            eventRepo.Delete(id);
        }

        public RentalEvent Read(int id)
        {
            var rentalEvent = eventRepo.Read(id);
            if (rentalEvent == null)
            {
                throw new ArgumentException("Car not exists");
            }
            return rentalEvent;
        }

        public IEnumerable<RentalEvent> ReadAll()
        {
            if (eventRepo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return eventRepo.ReadAll();
        }

        public void Update(RentalEvent item)
        {
            if (item.CustomerName.Length < 3 && item.Id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            eventRepo.Update(item);
        }

        // The most rented car in a certain period
        public MostRentedCarInfo GetMostRentedCar(DateTime startDate, DateTime endDate)
        {
            var mostRentedCar = eventRepo.ReadAll()
                              .Where(re => re.RentalDate >= startDate && re.RentalDate <= endDate)
                              .GroupBy(re => re.CarId)
                              .OrderByDescending(g => g.Count())
                              .Select(g => new
                              {
                                  Car = carRepo.Read(g.Key),
                                  RentalDate = eventRepo.ReadAll().Where(t => t.CarId == g.Key).FirstOrDefault().RentalDate,
                                  ReturnDate = eventRepo.ReadAll().Where(t => t.CarId == g.Key).FirstOrDefault().ReturnDate
                              })
                              .FirstOrDefault();

            MostRentedCarInfo mostRentedCarInfo = new MostRentedCarInfo
            {
                Car = mostRentedCar.Car,
                RentalTime = CalculateRentalTime(mostRentedCar.RentalDate, mostRentedCar.ReturnDate, startDate, endDate)
            };

            if (mostRentedCar != null)
            {
                return mostRentedCarInfo;
            }
            else
            {
                throw new ArgumentException("No car found in this peroid");
            }
        }

        private TimeSpan CalculateRentalTime(DateTime rentalDate, DateTime returnDate, DateTime startDate, DateTime endDate)
        {
            TimeSpan totalRentalTime = TimeSpan.Zero;

            var rentalStartDate = rentalDate > startDate ? rentalDate : startDate;
            var rentalEndDate = returnDate < endDate ? returnDate : endDate;

            totalRentalTime += rentalEndDate - rentalStartDate;

            return totalRentalTime;
        }

        public class MostRentedCarInfo
        {
            public Car Car { get; set; }
            public TimeSpan RentalTime { get; set; }
        }


    }
}
