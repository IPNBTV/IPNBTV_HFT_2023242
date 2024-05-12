using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IPNBTV_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        ICarLogic carLogic;
        IRentalEventLogic eventLogic;
        IRentalShopLogic shopLogic;

        public NonCrudController(ICarLogic carLogic, IRentalEventLogic eventLogic, IRentalShopLogic shopLogic)
        {
            this.carLogic = carLogic;
            this.eventLogic = eventLogic;
            this.shopLogic = shopLogic;
        }

        [HttpGet("{rentalShopId}")]
        public IEnumerable<MostRentedCarInfo> GetMostRentedCarInRentalShop(int rentalShopId)
        {
            return carLogic.GetMostRentedCarInRentalShop(rentalShopId);
        }

        [HttpGet("{carId}/{rentalShopId}")]
        public int GetNumberOfRentalEventsForCarInShop(int carId, int rentalShopId)
        {
            return carLogic.GetNumberOfRentalEventsForCarInShop(carId, rentalShopId);
        }

        [HttpGet("{startDate}/{endDate}")]
        public IEnumerable<MostRentedCarInfo> OrderByMostRentals(string startDate, string endDate)
        {
            return eventLogic.OrderByMostRentals(DateTime.Parse(startDate), DateTime.Parse(endDate));
        }

        [HttpGet]
        public IEnumerable<string> GetCustomersWithAutomaticTransmission()
        {
            return eventLogic.GetCustomersWithAutomaticTransmission();
        }

        [HttpGet("{color}")]
        public IEnumerable<RentalShop> GetRentalShopsWithCarColor(string color)
        {
            return shopLogic.GetRentalShopsWithCarColor(color);
        }
    }
}



