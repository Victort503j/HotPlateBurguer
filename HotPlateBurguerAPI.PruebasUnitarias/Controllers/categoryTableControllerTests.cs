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
using static HotPlateRestaurant.EN.categoryTable;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace HotPlateRestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class categoryTableControllerTests
    {
        [TestMethod()]
        public async Task GetTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var categories = await _httpClient.GetFromJsonAsync<categoryTable[]>("api/categoryTable");
        }

        [TestMethod()]
        public async Task GetTest1()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var categories = await _httpClient.GetFromJsonAsync<categoryTable>("api/categoryTable/1");
        }

        [TestMethod()]
        public async Task PostTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpCliente = aplication.CreateClient();
            var category = new categoryTable
            {
                Name = "Test",
                Icon = "Test"
            };
            var result = await _httpCliente.PostAsJsonAsync<categoryTable>("api/categoryTable", category);
        }

        [TestMethod()]
        public async Task PutTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpCliente = aplication.CreateClient();
            var category = new categoryTable
            {
                Id = 12,
                Name = "Test",
                Icon = "Test"
            };
            var result = await _httpCliente.PutAsJsonAsync<categoryTable>("api/categoryTable/" + category.Id, category);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var category = new categoryTable
            {
                Id = 12
            };
            var result = await _httpClient.DeleteAsync("api/categoryTable/" + category.Id);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            using var aplication = new WebApplicationFactory<Program>();
            using var _httpClient = aplication.CreateClient();
            var category = new categoryTable
            {
                Id = 5
            };
        }
    }
}