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
    public class categoryTableBLTests
    {

        private static categoryTable categoriaInicial = new categoryTable { Id = 6 };
        private categoryTableBL categoryTableBL = new categoryTableBL();
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var category = new categoryTable();
            category.Name = "Stake";
            category.Icon = "Test";
            int result = await categoryTableBL.CrearAsync(category);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var category = new categoryTable();
            category.Id = categoriaInicial.Id;
            category.Name = "Wraps";
            category.Icon = "Test";
            int result = await categoryTableBL.ModificarAsync(category);
            Assert.AreEqual(1, result);
        }


        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var category = new categoryTable();
            category.Id = categoriaInicial.Id;
            var result = await categoryTableBL.ObtenerPorIdAsync(category);
            Assert.AreEqual(category.Id, result.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var result = await categoryTableBL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var category = new categoryTable();
            category.Name = "Desserts";
            category.Top_Aux = 10;
            var resultCategory = await categoryTableBL.BuscarAsync(category);
            Assert.AreNotEqual(0, resultCategory.Count);
        }

        [TestMethod()]
        public async Task T6DeleteAsyncTest()
        {
            var category = new categoryTable();
            category.Id = categoriaInicial.Id;
            var result = await categoryTableBL.DeleteAsync(category);
            Assert.AreNotEqual(0, result);
        }
    }
}