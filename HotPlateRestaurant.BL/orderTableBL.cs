using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;

namespace HotPlateRestaurant.BL
{
    public class orderTableBL
    {
        public async Task<int> CrearAsync(orderTable pOrderTable)
        {
            return await orderTableDAL.CrearAsync(pOrderTable);
        }

        public async Task<int> ModificarAsync(orderTable pOrderTable)
        {
            return await orderTableDAL.ModificarAsync(pOrderTable);
        }

        public async Task<int> DeleteAsync(orderTable pOrderTable)
        {
            return await orderTableDAL.EliminarAsync(pOrderTable);
        }

        public async Task<orderTable> ObtenerPorIdAsync(orderTable pOrderTable)
        {
            return await orderTableDAL.ObtenerPorIdAsync(pOrderTable);
        }


        public async Task<List<orderTable>> ObtenerTodosAsync()
        {
            return await orderTableDAL.ObtenerTodosAsync();
        }

        public async Task<List<orderTable>> BuscarAsync(orderTable pOrderTable)
        {
            return await orderTableDAL.BuscarAsync(pOrderTable);
        }

    }
}
