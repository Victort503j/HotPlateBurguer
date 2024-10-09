using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MySqlConnector;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotPlateRestaurantAPI.Controllers
{
    [AllowAnonymous]
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

        [HttpGet("GetByCategory/{id}")]
        public async Task<IEnumerable<foodTable>> GetByCategory(int id)
        {
            return await foodTableBL.ObtenerTodosPorCategoriasAsync(new categoryTable { Id = id});
        }


        // GET api/<foodTableController>/5
        [HttpGet("{id}")]
        public async Task<foodTable> Get(int id)
        {
            return await foodTableBL.ObtenerPorIdAsync(new foodTable { Id = id });
        }

        // POST api/<foodTableController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] string pFoodTable, [FromForm] string pDataImages, [FromForm] IFormFile file)
        {
            // Inicializa Cloudinary con las credenciales correctas
            Account account = new Account(
                "dktn6oanx",    // Nombre de tu cloud
                "232768923435977",       // Tu API Key
                "QyzGFArBHXeH_RUiSVkwSJ6ojKE"     // Tu API Secret
            );

            Cloudinary cloudinary = new Cloudinary(account);

            try
            {
                // Verificar si el archivo es nulo o está vacío
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No se subió ningún archivo.");
                }

                // Generar el publicId basado en el nombre del archivo
                var publicId = $"food_images/{file.FileName}";

                // Intentar obtener la imagen existente de Cloudinary
                var existingImage = await cloudinary.GetResourceAsync(publicId);

                string imageUrl;

                if (existingImage != null && !string.IsNullOrEmpty(existingImage.SecureUrl))
                {
                    // Si la imagen ya existe, usa la URI existente
                    imageUrl = existingImage.SecureUrl;
                }
                else
                {
                    // Si no existe, sube el archivo a Cloudinary
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),
                            PublicId = publicId, // Usa el mismo publicId
                            Overwrite = true // Sobrescribir si ya existe
                        };

                        // Usar await para hacer la subida de forma asincrónica
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);

                        // Verificar que la subida haya sido exitosa
                        if (uploadResult == null || string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
                        {
                            throw new Exception("La subida de la imagen a Cloudinary falló.");
                        }

                        imageUrl = uploadResult.SecureUrl.ToString(); // Obtener la URI de la nueva imagen subida
                    }
                }

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                // Asegúrate de que la deserialización sea correcta
                FoodImages foodImages = JsonSerializer.Deserialize<FoodImages>(pDataImages, option);
                foodTable foodTable = JsonSerializer.Deserialize<foodTable>(pFoodTable, option);

                // Verificar que las deserializaciones no sean nulas
                if (foodImages == null)
                {
                    throw new ArgumentException("La deserialización de FoodImages falló.");
                }
                if (foodTable == null)
                {
                    throw new ArgumentException("La deserialización de foodTable falló.");
                }

                // Llama a tu lógica de negocio para guardar la información
                await foodTableBL.CrearAsync(foodTable, foodImages, imageUrl, publicId);

                return Ok(new { Message = "Imagen procesada correctamente", ImageUri = imageUrl });
            }
            catch (MySqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // Manejo de excepciones generales
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
