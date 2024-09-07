using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class foodTableController : ControllerBase
    {
        private foodTableBL foodTableBL = new foodTableBL();
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<foodTable>> Get()
        {
            foodTable foodTable = new foodTable();
            return await foodTableBL.ObtenerTodosAsync();
        }


        // GET api/<foodTableController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<foodTableController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<foodTableController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<foodTableController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
