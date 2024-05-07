using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace IPNBTV_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalEventController : ControllerBase
    {
        IRentalEventLogic logic;

        public RentalEventController(IRentalEventLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<RentalEvent> ReadAll()
        {
            return logic.ReadAll();
        }


        [HttpGet("{id}")]
        public RentalEvent Read(int id)
        {
            return logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] RentalEvent value)
        {
            logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] RentalEvent value)
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
