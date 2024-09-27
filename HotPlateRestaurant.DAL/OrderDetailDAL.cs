using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;

namespace HotPlateRestaurant.DAL
{
    public class OrderDetailDAL
    {
        public static async Task<List<OrderDetail>> ObtenerTodosAsync()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            using (var dbContexto = new DBContexto())
            {
                orderDetails = await dbContexto.orderDetail
                    .Include(od => od.OrderTable)
                    .Include(od => od.FoodTable)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            return orderDetails;
        }
    }
}
