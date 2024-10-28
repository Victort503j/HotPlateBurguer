using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using HotPlateRestaurant.EN.Payments;
using System.Net;
using System.Numerics;
using HotPlateRestaurant.EN;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace HotPlateRestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class orderTableControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task PostTest()
        {
            using var application = new WebApplicationFactory<Program>();
            using var _httpClient = application.CreateClient();
            var orderdetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    FoodTableId = 135,
                    Quantity = 5,
                    Price = 100
                }
            };
            var order = new
            {
                CustomerName = "test",
                Total = 10.5,
                Address = "AddresTest",
                Email = "test@gmail.com",
                Phone = "76313322",
                Orders = "test",
                orderDetails = orderdetails
            };
            var response = await _httpClient.PostAsJsonAsync("api/orderTable", order);
        }

        [TestMethod()]
        public void PutTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task BuscarTest()
        {
            using var application = new WebApplicationFactory<Program>();
            using var _httpClient = application.CreateClient();
            var order = new orderTable
            {
                CustomerName = "test"
            };
            var result = await _httpClient.PostAsJsonAsync("orderTable/Buscar", order);
        }
    }
}