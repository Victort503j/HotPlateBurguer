using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.BL
{
    public class foodTableBL
    {

        public async Task<int> CrearAsync(foodTable pFoodTable, FoodImages pFoodImages, string urlImage, string publicId)
        {
            return await foodTableDAL.CrearAsync(pFoodTable, pFoodImages, urlImage, publicId);
        }

        public async Task<int> ModificarAsync(foodTable pFoodTable)
        {
            return await foodTableDAL.ModificarAsync(pFoodTable);
        }

        public async Task<int> DeleteAsync(foodTable pFoodTable)
        {
            return await foodTableDAL.DeleteAsync(pFoodTable);
        }

        public async Task<foodTable> ObtenerPorIdAsync(foodTable pFoodTable)
        {
            return await foodTableDAL.ObtenerPorIdAsync(pFoodTable);
        }

        public async Task<List<foodTable>> ObtenerTodosAsync()
        {
            return await foodTableDAL.ObtenerTodosAsync();
        }

        public async Task<List<foodTable>> ObtenerTodosPorCategoriasAsync(categoryTable pCategoryTable)
        {
            return await foodTableDAL.ObtenerTodosPorCategoriasAsync(pCategoryTable);
        }

        public async Task<List<foodTable>> BuscarAsync(foodTable pFoodTable)
        {
            return await foodTableDAL.BuscarAsync(pFoodTable);
        }
    }
}
