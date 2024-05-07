using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace IPNBTV_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalShopController : ControllerBase
    {
        IRentalShopLogic logic;

        public RentalShopController(IRentalShopLogic logic)
        {
            this.logic = logic;
        }


        [HttpGet]
        public IEnumerable<RentalShop> ReadAll()
        {
            return logic.ReadAll();
        }


        [HttpGet("{id}")]
        public RentalShop Read(int id)
        {
            return logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] RentalShop value)
        {
            logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] RentalShop value)
        {
            logic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
