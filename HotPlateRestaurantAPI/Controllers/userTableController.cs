using Microsoft.AspNetCore.Mvc;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using System.Text.Json;
using HotPlateRestaurant.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userTableController : ControllerBase
    {
        private userTableBL userTableBl = new userTableBL();
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<userTable>> Get()
        {
            userTable usuario = new userTable();
            return await userTableBl.ObtenerTodosAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<userTable> Get(int id)
        {
            return await userTableBl.ObtenerPorIdAsync(new userTable { Id = id });
        }


        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Object pUserTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strUser = JsonSerializer.Serialize(pUserTable);
                userTable user = JsonSerializer.Deserialize<userTable>(strUser, option);
                await userTableBl.CrearAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUserTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strUser = JsonSerializer.Serialize(pUserTable);
                userTable user = JsonSerializer.Deserialize<userTable>(strUser, option);
                if (user.Id == id)
                {
                    await userTableBl.ModificarAsync(user);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await userTableBl.DeleteAsync(new userTable { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Buscar")]
        public async Task<List<userTable>> Buscar([FromBody] object pUserTable)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strUserTable = JsonSerializer.Serialize(pUserTable);
                userTable user = JsonSerializer.Deserialize<userTable>(strUserTable, option);

                // Llamar al método de búsqueda en el servicio
                return await userTableBl.BuscarAsync(user);
            }
            catch (JsonException jsonEx)
            {
                // Manejo de errores de deserialización
                Console.WriteLine($"Error de deserialización: {jsonEx.Message}");
                return new List<userTable>();
            }
            catch (Exception ex)
            {
                // Manejo de errores genéricos
                Console.WriteLine($"Error en la búsqueda: {ex.Message}");
                return new List<userTable>();
            }
        }
    }
}
