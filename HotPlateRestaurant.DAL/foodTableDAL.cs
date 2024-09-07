using HotPlateRestaurant.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace HotPlateRestaurant.DAL
{
    public class foodTableDAL
    {
        public static async Task<List<foodTable>> ObtenerTodosAsync()
        {
            List<foodTable> foodTables = new List<foodTable>();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    foodTables = await dbContexto.Foods.ToListAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error interno");
            }
            return foodTables;
        }
    }
}
