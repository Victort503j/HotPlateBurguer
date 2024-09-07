using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.BL
{
    public class foodTableBL
    {
        public async Task<List<foodTable>> ObtenerTodosAsync()
        {
            return await foodTableDAL.ObtenerTodosAsync();
        }
    }
}
