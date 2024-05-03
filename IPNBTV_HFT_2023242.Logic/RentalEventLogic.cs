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
        IRepository<RentalEvent> repo;

        public RentalEventLogic(IRepository<RentalEvent> repo)
        {
            this.repo = repo;
        }

        public void Create(RentalEvent item)
        {
            if (item.CustomerName.Length < 3)
            {
                throw new ArgumentException("Invalid Id");
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

        public RentalEvent Read(int id)
        {
            var rentalEvent = repo.Read(id);
            if (rentalEvent == null)
            {
                throw new ArgumentException("Car not exists");
            }
            return rentalEvent;
        }

        public IEnumerable<RentalEvent> ReadAll()
        {
            if (repo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return repo.ReadAll();
        }

        public void Update(RentalEvent item)
        {
            if (item.CustomerName.Length < 3 && item.Id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            repo.Update(item);
        }
    }
}
