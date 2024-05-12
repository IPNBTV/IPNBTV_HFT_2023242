using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Logic
{
    public class MostRentedCarInfo
    {
        public Car Car { get; set; }
        public int NumberOfRentalEvents { get; set; }

        public override bool Equals(object obj)
        {
            MostRentedCarInfo b = obj as MostRentedCarInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return Car.Id == b.Car.Id
                    && Car.Brand == b.Car.Brand
                    && Car.Model == b.Car.Model
                    && Car.Year == b.Car.Year
                    && Car.Color == b.Car.Color
                    && Car.IsAutomatic == b.Car.IsAutomatic
                    && Car.RentalShopId == b.Car.RentalShopId
                    && NumberOfRentalEvents == b.NumberOfRentalEvents;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Car, NumberOfRentalEvents);
        }
    }
}
