using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace IPNBTV_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarLogic logic;

        public CarController(ICarLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Car> ReadAll()
        {
            return logic.ReadAll();
        }


        [HttpGet("{id}")]
        public Car Read(int id)
        {
            return logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Car value)
        {
            logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] Car value)
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
