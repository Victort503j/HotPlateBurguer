using Microsoft.AspNetCore.Mvc;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderTableController : ControllerBase
    {
        // GET: api/<orderTableController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<orderTableController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<orderTableController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<orderTableController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<orderTableController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
