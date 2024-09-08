using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.DAL
{
    class orderTableDAL
    {
        public static async Task<int> CrearAsync(orderTable pOrderTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    dbContexto.Add(pOrderTable);
                    result = await dbContexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar crear");
            }
            return result;
        }
        public static async Task<int> ModificarAsync(orderTable pOrderTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    var orderTable = await dbContexto.orderTable.FirstOrDefaultAsync(s => s.Confirmation_ID == pOrderTable.Confirmation_ID);
                   orderTable.CustomerName = pOrderTable.CustomerName;
                   orderTable.Total = pOrderTable.Total;
                   orderTable.OrderTime = pOrderTable.OrderTime;
                   orderTable.Address = pOrderTable.Address;
                   orderTable.Email = pOrderTable.Email;
                   orderTable.Phone = pOrderTable.Phone;
                    dbContexto.Update(orderTable);
                    result = await dbContexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error interno");
            }
            return result;
        }
    }
}
