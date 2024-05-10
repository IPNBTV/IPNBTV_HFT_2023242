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
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
