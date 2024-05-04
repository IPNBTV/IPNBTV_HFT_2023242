using IPNBTV_HFT_2023242.Models;
using System.Collections.Generic;

namespace IPNBTV_HFT_2023242.Logic
{
    public interface ICarLogic
    {
        void Create(Car item);
        void Delete(int id);
        Car Read(int id);
        IEnumerable<Car> ReadAll();
        void Update(Car item);
        IEnumerable<CarLogic.MostRentedCarInfo> GetMostRentedCarsInRentalShop(int rentalShopId);
        int GetNumberOfRentalEventsForCarInShop(int carId, int rentalShopId);
    }
}