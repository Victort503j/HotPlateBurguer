using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryTableController : ControllerBase
    {
        private categoryTableBL categorytableBl = new categoryTableBL();

        [HttpGet]
        public async Task<IEnumerable<categoryTable>> Get()
        {
            return await categorytableBl.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<categoryTable> Get(int id)
        {
            categoryTable categorytable = new categoryTable();
            categorytable.Id = id;
            return await categorytableBl.ObtenerPorIdAsync(categorytable);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object pCategoryTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string strCategoryTable = JsonSerializer.Serialize(pCategoryTable);
                categoryTable categorytable = JsonSerializer.Deserialize<categoryTable>(strCategoryTable, option);
                await categorytableBl.CrearAsync(categorytable);
                return Ok();
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pCategoryTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string strCategoryTable = JsonSerializer.Serialize(pCategoryTable);
                categoryTable categorytable = JsonSerializer.Deserialize<categoryTable>(strCategoryTable, option);
                if (categorytable.Id == id)
                {
                    await categorytableBl.ModificarAsync(categorytable);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await categorytableBl.DeleteAsync(new categoryTable { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Buscar")]
        public async Task<List<categoryTable>> Buscar([FromBody] object pCategoryTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strCategoryTable = JsonSerializer.Serialize(pCategoryTable);
                categoryTable rol = JsonSerializer.Deserialize<categoryTable>(strCategoryTable, option);
                return await categorytableBl.BuscarAsync(rol);
            }
            catch (Exception ex)
            {
                return new List<categoryTable>();
            }
        }
    }
}
