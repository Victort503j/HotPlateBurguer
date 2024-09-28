using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MySqlConnector;

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
        public async Task<ActionResult> Post([FromForm] string pFoodTable,[FromForm] string pDataImages, [FromForm]IFormFile file)
        {
            Cloudinary cloudinary = new Cloudinary();
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No file uploaded.");
                }

                var uploadResult = new ImageUploadResult();


                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = $"food_images/{file.FileName}", // Personaliza el PublicId
                        Overwrite = true // Sobrescribir si ya existe
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                //string srtFood = JsonSerializer.Serialize(pFoodTable);
                FoodImages foodImages =JsonSerializer.Deserialize<FoodImages>(pDataImages, option);
                foodTable foodTable = JsonSerializer.Deserialize<foodTable>(pFoodTable, option);

                await foodTableBL.CrearAsync(foodTable, foodImages, uploadResult.SecureUrl.ToString(), uploadResult.PublicId.ToString());
                return Ok();
            }
            catch (MySqlException ex)
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
