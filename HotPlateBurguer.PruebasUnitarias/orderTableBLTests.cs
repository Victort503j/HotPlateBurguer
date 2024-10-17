using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.EN;
using HotPlateRestaurant.EN.Payments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotPlateRestaurant.BL.Tests
{
    [TestClass()]
    public class OrderTableBLTests
    {
        private readonly orderTable _initialOrder = new orderTable
        {
            Confirmation_ID = 70,
            CustomerName = "Jose",
            Total = Convert.ToDecimal(20),
            Address = "Calle 5",
            Email = "joseromero@gmail.com",
            Phone = "3422-5355",
            Orders = "",
            orderDetails = new List<OrderDetail>
            {
                new OrderDetail { FoodTableId = 76, Quantity = 2, Price = Convert.ToDecimal(10) },
                new OrderDetail { FoodTableId = 77, Quantity = 1, Price = Convert.ToDecimal(10) }
            }
        };

        private readonly orderTableBL _orderBl = new orderTableBL();

        [TestMethod()]
        public async Task T1CrearAsync()
        {
            var newOrder = new orderTable
            {
                CustomerName = "Jose",
                Total = Convert.ToDecimal(20),
                Address = "Calle 5",
                Email = "joseromero@gmail.com",
                Phone = "3422-5355",
                Orders = "",
                orderDetails = new List<OrderDetail>
                {
                    new OrderDetail { FoodTableId = 76, Quantity = 2, Price = Convert.ToDecimal(10) },
                    new OrderDetail { FoodTableId = 77, Quantity = 1, Price = Convert.ToDecimal(10) }
                }
            };

            var result = await _orderBl.CrearAsync(newOrder);
            Assert.AreEqual(2, result, "The order was not created successfully.");
        }

        [TestMethod()]
        public async Task T2ModificarAsync()
        {
            var orderToUpdate = new orderTable
            {
                Confirmation_ID = _initialOrder.Confirmation_ID,
                CustomerName = _initialOrder.CustomerName,
                Total = _initialOrder.Total,
                Address = _initialOrder.Address,
                Email = _initialOrder.Email,
                Phone = _initialOrder.Phone,
                Orders = _initialOrder.Orders
            };

            var result = await _orderBl.ModificarAsync(orderToUpdate);
            Assert.AreEqual(1, result, "The order was not modified successfully.");
        }

        [TestMethod()]
        public async Task T3ObtenerTodosAsync()
        {
            var result = await _orderBl.ObtenerTodosAsync();
            Assert.IsTrue(result.Count > 0, "No orders were returned.");
        }

        [TestMethod()]
        public async Task T4ObtenerPorIdAsync()
        {
            var order = new orderTable
            {
                Confirmation_ID = _initialOrder.Confirmation_ID
            };

            var result = await _orderBl.ObtenerPorIdAsync(order);
            Assert.AreEqual(order.Confirmation_ID, result.Confirmation_ID, "The order ID does not match.");
        }

        [TestMethod()]
        public async Task T5BuscarAsync()
        {
            var searchOrder = new orderTable
            {
                CustomerName = _initialOrder.CustomerName,
                Top_Aux = 10
            };

            var result = await _orderBl.BuscarAsync(searchOrder);
            Assert.AreNotEqual(0, result.Count, "No matching orders were found.");
        }

        [TestMethod()]
        public async Task T6DeleteAsync()
        {
            var orderToDelete = new orderTable
            {
                Confirmation_ID = 70
            };

            var result = await _orderBl.DeleteAsync(orderToDelete);
            Assert.AreEqual(1, result, "The order was not deleted successfully.");
        }
    }
}
