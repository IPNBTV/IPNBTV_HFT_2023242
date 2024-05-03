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
        IRepository<RentalShop> repo;

        public RentalShopLogic(IRepository<RentalShop> repo)
        {
            this.repo = repo;
        }

        public void Create(RentalShop item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Name is too short");
            }
            repo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            repo.Delete(id);
        }

        public RentalShop Read(int id)
        {
            var rentalShop = repo.Read(id);
            if (rentalShop == null)
            {
                throw new ArgumentException("RentalShop not exists");
            }
            return rentalShop;
        }

        public IEnumerable<RentalShop> ReadAll()
        {
            if (repo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return repo.ReadAll();
        }

        public void Update(RentalShop item)
        {
            if (item.Name.Length < 3 && item.Id < 1)
            {
                throw new ArgumentException("Invalid item");
            }
            repo.Update(item);
        }
    }
}
