using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.DAL
{
    public class orderTableDAL
    {
        #region "CRUD"
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
        public static async Task<int> EliminarAsync(orderTable pOrderTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    var order = await dbContexto.orderTable.FirstOrDefaultAsync(
                        f => f.Confirmation_ID == pOrderTable.Confirmation_ID);
                    dbContexto.orderTable.Remove(order);
                    result = await dbContexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("An internal error ocurred");
            }
            return result;
        }
        public static async Task<orderTable> ObtenerPorIdAsync(orderTable pOrderTable)
        {
            var order = new orderTable();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    order = await dbContexto.orderTable.FirstOrDefaultAsync(
                        o => o.Confirmation_ID == pOrderTable.Confirmation_ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An internal error ocurred");
            }
            return order;
        }
        public static async Task<List<orderTable>> ObtenerTodosAsync()
        {
            List<orderTable> orders = new List<orderTable>();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    orders = await dbContexto.orderTable.ToListAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An internal error ocurred");
            }
            return orders;
        }
        internal static IQueryable<orderTable> QuerySelect(IQueryable<orderTable> pQuery,
           orderTable pOrderTable)
        {
            if (pOrderTable.Confirmation_ID > 0)
                pQuery = pQuery.Where(s => s.Confirmation_ID == pOrderTable.Confirmation_ID);
            if (!string.IsNullOrWhiteSpace(pOrderTable.Orders))
                pQuery = pQuery.Where(s => s.Total == pOrderTable.Total);
            if (pOrderTable.OrderTime.Year > 1000)
            {
                DateTime initialDate = new DateTime(pOrderTable.OrderTime.Year,
                    pOrderTable.OrderTime.Month, pOrderTable.OrderTime.Day, 0, 0, 0);
                DateTime fechaFinal = initialDate.AddDays(-3).AddMilliseconds(1);
                pQuery = pQuery.Where(s => s.OrderTime >= initialDate &&
                s.OrderTime <= fechaFinal);
            }
            if (!string.IsNullOrWhiteSpace(pOrderTable.Address))
                pQuery = pQuery.Where(s => s.Address.Contains(pOrderTable.Address));
            if (!string.IsNullOrWhiteSpace(pOrderTable.Email))
                pQuery = pQuery.Where(s => s.Email == pOrderTable.Email);
            if (!string.IsNullOrWhiteSpace(pOrderTable.Phone))
                pQuery = pQuery.Where(s => s.Phone == pOrderTable.Phone);
            if (!string.IsNullOrWhiteSpace(pOrderTable.Orders))
                pQuery = pQuery.Where(s => s.Orders == pOrderTable.Orders);
            pQuery = pQuery.OrderByDescending(s => s.Confirmation_ID).AsQueryable();
            if (pOrderTable.Top_Aux > 0)
                pQuery = pQuery.Take(pOrderTable.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<orderTable>> BuscarAsync(orderTable pOrderTable)
        {
            var orders = new List<orderTable>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.orderTable.AsQueryable();
                select = QuerySelect(select, pOrderTable);
                orders = await select.ToListAsync();
            }
            return orders;
        }
    #endregion

    }

}
