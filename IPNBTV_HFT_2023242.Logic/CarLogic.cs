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
        IRepository<Car> repo;

        public CarLogic(IRepository<Car> repo)
        {
            this.repo = repo;
        }

        public void Create(Car item)
        {
            if (item.Id < 1)
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

        public Car Read(int id)
        {
            var car = repo.Read(id);
            if (car == null)
            {
                throw new ArgumentException("Car not exists");
            }
            return car;
        }

        public IEnumerable<Car> ReadAll()
        {
            if (repo.ReadAll() == null)
            {
                throw new ArgumentException("Empty database");
            }
            return repo.ReadAll();
        }

        public void Update(Car item)
        {
            if (item.Id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            repo.Update(item);
        }
    }
}
