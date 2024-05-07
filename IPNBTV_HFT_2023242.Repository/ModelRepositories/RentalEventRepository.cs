using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Repository
{
    public class RentalEventRepository : Repository<RentalEvent>, IRepository<RentalEvent>
    {
        public RentalEventRepository(RentalShopDbContext ctx) : base(ctx)
        {
        }

        public override RentalEvent Read(int id)
        {
            return ctx.RentalEvents.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(RentalEvent item)
        {
            var old = Read(item.Id);
            
            old.Id = item.Id;
            old.CarId = item.CarId;
            old.RentalDate = item.RentalDate;
            old.ReturnDate = item.ReturnDate;
            old.CustomerName = item.CustomerName;

            ctx.SaveChanges();
        }
    }
}
