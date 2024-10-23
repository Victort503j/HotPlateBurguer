using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.EN;
using static System.Net.WebRequestMethods;
using HotPlateRestaurant.EN.Payments;
using static HotPlateRestaurant.EN.userTable;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace HotPlateRestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class userTableControllerTests
    {
        [TestMethod()]
        public async Task GetTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuarios = await _httpClient.GetFromJsonAsync<userTable[]>("api/userTable");
        }

        [TestMethod()]
        public async Task GetTest1()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuarios = await _httpClient.GetFromJsonAsync<userTable>("api/userTable/1");
        }

        [TestMethod()]
        public async Task PostTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuario = new userTable 
            {
                Name = "Test2",
                LastName = "Test2",
                Phone = "1233349",
                Email = "Test2",
                Password = "Test2",
                Status = (byte)Estatus_Usuario.ACTIVE
            };
            var resultado = await _httpClient.PostAsJsonAsync<userTable>("api/userTable", usuario);
        }

        [TestMethod()]
        public async Task PutTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuario = new userTable
            {
                Id = 48,
                Name = "Test3",
                LastName = "Test3",
                Phone = "1233499",
                Email = "Test9",
                Password = "Test977",
                Status = (byte)Estatus_Usuario.ACTIVE,
            };
            var resultado = await _httpClient.PutAsJsonAsync<userTable>("api/userTable/" + usuario.Id, usuario);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuario = new userTable
            {
                Id = 40
            };
            var result = await _httpClient.DeleteAsync("api/userTable/" + usuario.Id);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var usuario = new userTable
            {
                Id = 40
            };
        }
    }
}