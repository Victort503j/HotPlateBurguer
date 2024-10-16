using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurant.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.EN;

namespace HotPlateRestaurant.BL.Tests
{
    [TestClass()]
    public class foodTableBLTests
    {
        private foodTableBL foodBl = new foodTableBL();
        private foodTable foodInicail = new foodTable
        {
            Id = 133,
            Title = "Sandwich",
            Price = "2.00",
            Picture = "Sandwich.png",
            CategoryId = 1
        };
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var food = new foodTable
            {
                Title = "HotDog",
                Price = "3.00",
                Picture = "",
                CategoryId = 1
            };

            var image = new FoodImages
            {
                Title = "HotDog",
                AltText = "HotDog",
                IsPrimary = 1
            };

            string urlIMage = "hotdog.png";
            string publicId = "hotdogId1";
            var result = await foodBl.CrearAsync(food, image, urlIMage, publicId);
            Assert.AreEqual(2, result);
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var food = new foodTable
            {
                Id = foodInicail.Id,
                CategoryId = foodInicail.CategoryId,
                Title = foodInicail.Title,
                Price = foodInicail.Price,
                Picture = foodInicail.Picture,
            };

            var result = await foodBl.ModificarAsync(food);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void T3DeleteAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T4ObtenerPorIdAsyncTest()
        {
            foodTable foodId = new foodTable
            {
                Id = foodInicail.Id,
            };
            var result = await foodBl.ObtenerPorIdAsync(foodId);
            Assert.IsTrue(result.Id == foodId.Id);
        }

        [TestMethod()]
        public async Task T5ObtenerTodosAsyncTest()
        {
            var result = await foodBl.ObtenerTodosAsync();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod()]
        public async Task T6ObtenerTodosPorCategoriasAsyncTest()
        {
            var foodByCategory = new categoryTable
            {
                Id = 1
            };

            var result = await foodBl.ObtenerTodosPorCategoriasAsync(foodByCategory);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod()]
        public async Task T7BuscarAsyncTest()
        {
            var foodSearch = new foodTable
            {
                Id = 133,
                CategoryId = 1,
                Title = "Sandwich"
            };
            var result = await foodBl.BuscarAsync(foodSearch);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}