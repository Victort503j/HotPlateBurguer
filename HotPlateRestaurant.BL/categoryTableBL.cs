using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;

namespace HotPlateRestaurant.BL
{
    public class categoryTableBL
    {
        public async Task<int> CrearAsync(categoryTable pcategoryTable)
        {
            return await categoryTableDAL.CrearAsync(pcategoryTable);
        }

        public async Task<int> ModificarAsync(categoryTable pcategoryTable)
        {
            return await categoryTableDAL.ModificarAsync(pcategoryTable);
        }

        public async Task<int> DeleteAsync(categoryTable pcategoryTable)
        {
            return await categoryTableDAL.DeleteAsync(pcategoryTable);
        }

        public async Task<categoryTable> ObtenerPorIdAsync(categoryTable pCategoryTable)
        {
            return await categoryTableDAL.ObtenerPorIdAsync(pCategoryTable);
        }


        public async Task<List<categoryTable>> ObtenerTodosAsync()
        {
            return await categoryTableDAL.ObtenerTodosAsync();
        }

        public async Task<List<categoryTable>> BuscarAsync(categoryTable pcategoryTable)
        {
            return await categoryTableDAL.BuscarAsync(pcategoryTable);
        }

    }
}
