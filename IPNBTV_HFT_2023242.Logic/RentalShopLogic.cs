using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using System;
using System.Linq;

namespace IPNBTV_HFT_2023242.Logic
{
    public class RentalShopLogic
    {
        IRepository<RentalShop> repo;

        public RentalShopLogic(IRepository<RentalShop> repo)
        {
            this.repo = repo;
        }

        public void Create(RentalShop item)
        {
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public RentalShop Read(int id)
        {
            return repo.Read(id);
        }

        public IQueryable<RentalShop> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(RentalShop item)
        {
            repo.Update(item);
        }
    }
}
