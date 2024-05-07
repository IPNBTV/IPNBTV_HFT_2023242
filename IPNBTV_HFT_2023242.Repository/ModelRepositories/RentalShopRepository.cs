using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Repository
{
    public class RentalShopRepository : Repository<RentalShop>, IRepository<RentalShop>
    {
        public RentalShopRepository(RentalShopDbContext ctx) : base(ctx)
        {
        }

        public override RentalShop Read(int id)
        {
            return ctx.RentalShops.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(RentalShop item)
        {
            var old = Read(item.Id);

            old.Id = item.Id;
            old.Name = item.Name;
            old.City = item.City;
            old.Address = item.Address;
            old.Phone = item.Phone;

            ctx.SaveChanges();
        }
    }
}
