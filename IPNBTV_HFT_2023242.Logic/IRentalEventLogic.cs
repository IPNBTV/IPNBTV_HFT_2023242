using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections.Generic;

namespace IPNBTV_HFT_2023242.Logic
{
    public interface IRentalEventLogic
    {
        void Create(RentalEvent item);
        void Delete(int id);
        RentalEvent Read(int id);
        IEnumerable<RentalEvent> ReadAll();
        void Update(RentalEvent item);
        IEnumerable<RentalEventLogic.MostRentedCarInfo> OrderByMostRentals(DateTime startDate, DateTime endDate);
        IEnumerable<string> GetCustomersWithAutomaticTransmission();
    }
}