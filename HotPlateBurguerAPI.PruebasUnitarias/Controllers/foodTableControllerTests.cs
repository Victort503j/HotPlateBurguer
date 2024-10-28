using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using HotPlateRestaurant.EN;
using System.Net.Http.Json;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HotPlateRestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class foodTableControllerTests
    {
        [TestMethod()]
        public async Task GetTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var foods = await _httpClient.GetFromJsonAsync<foodTable[]>("api/foodTable");
        }

        [TestMethod()]
        public async Task GetByCategoryTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var foods = await _httpClient.GetFromJsonAsync<foodTable[]>($"api/foodTable/GetByCategory/{2}");
        }

        [TestMethod()]
        public async Task GetTestById1()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var food = await _httpClient.GetFromJsonAsync<foodTable>("api/foodTable/" + 76);
        }

        [TestMethod()]
        public async Task PostTest()
        {
            using var application = new WebApplicationFactory<Program>();
            using var _httpClient = application.CreateClient();

            // Crear el contenido multipart para enviar los datos
            var content = new MultipartFormDataContent();

            // Serializar el objeto `foodTable` a JSON
            var food = new foodTable
            {
                CategoryId = 1,
                Title = "Test",
                Price = "2.00",
                Picture = "test.png"
            };
            var foodJson = JsonConvert.SerializeObject(food);
            content.Add(new StringContent(foodJson), "pFoodTable");

            // Serializar el objeto `FoodImages` a JSON
            var imagefood = new FoodImages
            {
                Title = "Test",
                AltText = "Test"
            };
            var imageJson = JsonConvert.SerializeObject(imagefood);
            content.Add(new StringContent(imageJson), "pDataImages");

            // Agregar el archivo de imagen
            var filePath = "C:\\Users\\henry\\Downloads/Oreo Cake.jpg"; // Cambia esta ruta a la ubicación del archivo en tu sistema
            using var fileStream = File.OpenRead(filePath);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // Especifica el tipo MIME
            content.Add(fileContent, "file", "Oreo Cake.jpg");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/foodTable", content);
        }

        [TestMethod()]
        public async Task PutTest()
        {
            using var application = new WebApplicationFactory<Program>();
            using var _httpClient = application.CreateClient();
            var food = new foodTable
            {
                Id = 135,
                CategoryId = 3,
                Title = "Test Actualizado",
                Price = "5.00"
            };

            var resultado = await _httpClient.PutAsJsonAsync("api/foodTable/" + 135, food);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            //using var application = new WebApplicationFactory<Program>();
            //using var _httpClient = application.CreateClient();
            //var food = await _httpClient.GetFromJsonAsync<foodTable>("api/foodTable/" + 135);
        }

        [TestMethod()]
        public async Task BuscarTest()
        {
            using var application = new WebApplicationFactory<Program>();
            using var _httpClient = application.CreateClient();
            var food = new foodTable
            {
                Title= "Test",
            };
            var result = await _httpClient.PostAsJsonAsync("api/foodTable/Buscar", food);
        }
    }
}