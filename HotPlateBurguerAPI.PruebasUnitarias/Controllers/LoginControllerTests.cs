using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using HotPlateRestaurant.EN;

namespace HotPlateRestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public async Task LoginTestAPI()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var login = new userTable {Email = "manuel@gmail.com", Password = "P@ssWord#123" };
            var response = await _httpClient.PostAsJsonAsync("login", login);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Manejar error de autenticación aquí, como mostrar un mensaje al usuario
                Console.WriteLine("Login failed.");
            }
        }
    }
}