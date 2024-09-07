using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryTableController : ControllerBase
    {
        // GET: api/<categoryTableController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<categoryTableController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<categoryTableController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<categoryTableController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<categoryTableController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
