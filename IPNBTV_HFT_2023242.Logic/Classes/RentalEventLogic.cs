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
                throw new ArgumentException("Name is too short");
            }
            else if (item.Id < 1)
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

        public IEnumerable<MostRentedCarInfo> OrderByMostRentals(DateTime startDate, DateTime endDate)
        {
            var result = eventRepo.ReadAll()
                              .Where(t => t.RentalDate >= startDate && t.RentalDate <= endDate)
                              .GroupBy(t => t.CarId)
                              .OrderByDescending(g => g.Count())
                              .Select(g => new MostRentedCarInfo
                              {
                                  Car = carRepo.Read(g.Key),
                                  NumberOfRentalEvents = g.Count()
                              });

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new ArgumentException("No car found in this peroid");
            }
        }

        public IEnumerable<string> GetCustomersWithAutomaticTransmission()
        {
            var result = eventRepo.ReadAll()
                            .Where(t => t.Car.IsAutomatic)
                            .Select(t => t.CustomerName)
                            .Distinct();

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new ArgumentException("No customer found with automatic car");
            }
        }

        public class MostRentedCarInfo
        {
            public Car Car { get; set; }
            public int NumberOfRentalEvents { get; set; }
        }
    }
}
