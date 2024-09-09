using HotPlateRestaurant.BL;
using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderTableController : ControllerBase
    {
        private orderTableBL orderTableBl = new orderTableBL();
        [HttpGet]
        public async Task<IEnumerable<orderTable>> Get()
        {
            orderTable ordertable = new orderTable();
            return await orderTableBl.ObtenerTodosAsync();
        }


        [HttpGet("{id}")]
        public async Task<orderTable> Get(int id)
        {
            return await orderTableBl.ObtenerPorIdAsync(new orderTable { Confirmation_ID = id });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object pOrderTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string strOrder = JsonSerializer.Serialize(pOrderTable);
                orderTable ordertable = JsonSerializer.Deserialize<orderTable>(strOrder, option);
                await orderTableBl.CrearAsync(ordertable);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pOrderTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string strOrder = JsonSerializer.Serialize(pOrderTable);
                orderTable ordertable = JsonSerializer.Deserialize<orderTable>(strOrder, option);
                if (ordertable.Confirmation_ID == id)
                {
                    await orderTableBl.ModificarAsync(ordertable);
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

        // DELETE api/<orderTableController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await orderTableBl.DeleteAsync(new orderTable { Confirmation_ID = id });
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Buscar")]
        public async Task<List<orderTable>> Buscar([FromBody] object pOrderTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strOrderTable = JsonSerializer.Serialize(pOrderTable);
                orderTable order = JsonSerializer.Deserialize<orderTable>(strOrderTable, option);
                return await orderTableBl.BuscarAsync(order);
            }
            catch (Exception ex)
            {
                return new List<orderTable>();
            }
        }
    }
}
