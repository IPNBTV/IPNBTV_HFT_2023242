using IPNBTV_HFT_2023242.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static IPNBTV_HFT_2023242.Logic.CarLogic;

namespace IPNBTV_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudCarController : ControllerBase
    {
        ICarLogic logic;

        public NonCrudCarController(ICarLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{rentalShopId}")]
        public IEnumerable<MostRentedCarInfo> GetMostRentedCarInRentalShop(int rentalShopId)
        {
            return logic.GetMostRentedCarInRentalShop(rentalShopId);
        }

        [HttpGet("{carId}/{rentalShopId}")]
        public int GetNumberOfRentalEventsForCarInShop(int carId, int rentalShopId)
        {
            return logic.GetNumberOfRentalEventsForCarInShop(carId, rentalShopId);
        }
    }
}
