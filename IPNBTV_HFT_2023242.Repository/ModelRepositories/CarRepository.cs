using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Repository
{
    public class CarRepository : Repository<Car>, IRepository<Car>
    {
        public CarRepository(RentalShopDbContext ctx) : base(ctx)
        {
        }

        public override Car Read(int id)
        {
            return ctx.Cars.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Car item)
        {
            var old = Read(item.Id);

            old.Id = item.Id;
            old.Brand = item.Brand;
            old.Model = item.Model;
            old.Year = item.Year;
            old.Color = item.Color;
            old.IsAutomatic = item.IsAutomatic;
            old.RentalShopId = item.RentalShopId;

            ctx.SaveChanges();
        }
    }
}
