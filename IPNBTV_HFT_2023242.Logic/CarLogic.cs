using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Logic
{
    public class CarLogic : ICarLogic
    {
        IRepository<Car> carRepo;

        public CarLogic(IRepository<Car> carRepo)
        {
            this.carRepo = carRepo;
        }

        public void Create(Car item)
        {
            if (item.Id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            carRepo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            carRepo.Delete(id);
        }

        public Car Read(int id)
        {
            var car = carRepo.Read(id);
            if (car == null)
            {
                throw new ArgumentException("Car not exists");
            }
            return car;
        }

        public IEnumerable<Car> ReadAll()
        {
            if (carRepo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return carRepo.ReadAll();
        }

        public void Update(Car item)
        {
            if (item.Id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            carRepo.Update(item);
        }

        public IEnumerable<MostRentedCarInfo> GetMostRentedCarsInRentalShop(int rentalShopId)
        {
            if (rentalShopId > 0)
            {
                var result = carRepo.ReadAll()
                                .Where(t => t.RentalShopId == rentalShopId)
                                .GroupBy(t => t.Id)
                                .OrderByDescending(g => carRepo.Read(g.Key).RentalEvents.Count())
                                .Select(g => new MostRentedCarInfo
                                {
                                    Car = carRepo.Read(g.Key),
                                    NumberOfRentalEvents = carRepo.Read(g.Key).RentalEvents.Count()
                                })
                                .Take(1);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new ArgumentException("No car found in rentalshop");
                }
            }
            else
            {
                throw new ArgumentException("Ivalid rentalshopId");
            }
        }

        public class MostRentedCarInfo
        {
            public Car Car { get; set; }
            public int NumberOfRentalEvents { get; set; }
        }
    }
}
