using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPNBTV_HFT_2023242.Logic
{
    public class RentalShopLogic : IRentalShopLogic
    {
        IRepository<RentalShop> shopRepo;
        IRepository<Car> carRepo;

        public RentalShopLogic(IRepository<RentalShop> shopRepo, IRepository<Car> carRepo)
        {
            this.shopRepo = shopRepo;
            this.carRepo = carRepo;
        }

        public void Create(RentalShop item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Name is too short");
            }
            shopRepo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            shopRepo.Delete(id);
        }

        public RentalShop Read(int id)
        {
            var rentalShop = shopRepo.Read(id);
            if (rentalShop == null)
            {
                throw new ArgumentException("RentalShop not exists");
            }
            return rentalShop;
        }

        public IEnumerable<RentalShop> ReadAll()
        {
            if (shopRepo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return shopRepo.ReadAll();
        }

        public void Update(RentalShop item)
        {
            if (item.Name.Length < 3 && item.Id < 1)
            {
                throw new ArgumentException("Invalid item");
            }
            shopRepo.Update(item);
        }

        public IEnumerable<RentalShop> GetRentalShopsWithCarColor(string color)
        {
            var result = carRepo.ReadAll()
                                .Where(t => t.Color == color)
                                .Select(t => new RentalShop
                                {
                                    Id = t.RentalShopId,
                                    Name = shopRepo.Read(t.RentalShopId).Name,
                                    City = shopRepo.Read(t.RentalShopId).City,
                                    Address = shopRepo.Read(t.RentalShopId).Address,
                                    Phone = shopRepo.Read(t.RentalShopId).Phone,
                                    Cars = shopRepo.Read(t.RentalShopId).Cars
                                });

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new ArgumentException("No rental shop found with this color car");
            }
        }
    }
}
