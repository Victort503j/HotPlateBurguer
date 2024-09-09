using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public async Task<foodTable> Get(int id)
        {
            return await foodTableBL.ObtenerPorIdAsync(new foodTable { Id = id });
        }

        // POST api/<foodTableController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object pFoodTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string srtFood = JsonSerializer.Serialize(pFoodTable);
                foodTable foodTable = JsonSerializer.Deserialize<foodTable>(srtFood, option);
                await foodTableBL.CrearAsync(foodTable);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<foodTableController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pFoodTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string srtFood = JsonSerializer.Serialize(pFoodTable);
                foodTable foodTable = JsonSerializer.Deserialize<foodTable>(srtFood, option);
                if (foodTable.Id == id)
                {
                    await foodTableBL.ModificarAsync(foodTable);
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<foodTableController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await foodTableBL.DeleteAsync(new foodTable { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<foodTable>> Buscar([FromBody] object pFoodTable)
        {

            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strFood = JsonSerializer.Serialize(pFoodTable);
                foodTable food = JsonSerializer.Deserialize<foodTable>(strFood, option);
                return await foodTableBL.BuscarAsync(food);
            }
            catch (Exception ex)
            {
                return new List<foodTable>();
            }
        }
    }
}
