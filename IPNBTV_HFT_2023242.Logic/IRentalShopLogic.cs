using IPNBTV_HFT_2023242.Models;
using System.Collections.Generic;

namespace IPNBTV_HFT_2023242.Logic
{
    public interface IRentalShopLogic
    {
        void Create(RentalShop item);
        void Delete(int id);
        RentalShop Read(int id);
        IEnumerable<RentalShop> ReadAll();
        void Update(RentalShop item);
    }
}