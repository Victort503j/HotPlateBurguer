using HotPlateRestaurant.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


namespace HotPlateRestaurant.DAL
{
    public class foodTableDAL
    {

        public static async Task<int> CrearAsync(foodTable pFoodTable, FoodImages pFoodImages, string urlImage, string publicId)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    // Iniciar la transacción
                    using (var transaction = await dbContexto.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            // 1. Guardar la comida
                            var food = new foodTable
                            {
                                Title = pFoodTable.Title,
                                Price = pFoodTable.Price,
                                Picture = urlImage,
                                CategoryId = pFoodTable.CategoryId,
                            };
                            dbContexto.Add(food);
                            result += await dbContexto.SaveChangesAsync();

                            // 2. Guardar la imagen relacionada
                            var resultImage = new FoodImages
                            {
                                FoodId = food.Id, // Asigna el ID de food
                                Title = pFoodImages.Title,
                                AltText = pFoodImages.AltText,
                                ImageUrl = urlImage,
                                PublicId = publicId,
                                foodTable = food,
                                IsPrimary = pFoodImages.IsPrimary,
                            };
                            dbContexto.foodimages.Add(resultImage);
                            result += await dbContexto.SaveChangesAsync();

                            // Confirmar la transacción si todo salió bien
                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            // Si ocurre un error, revertir la transacción
                            await transaction.RollbackAsync();
                            throw new Exception($"Error al guardar: {ex.Message}");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error de MySQL: {ex.Message}");
            }
            return result;
        }



        public static async Task<int> ModificarAsync(foodTable pFoodTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    var foodTable = await dbContexto.foodTable.FirstOrDefaultAsync(s => s.Id == pFoodTable.Id);
                    foodTable.CategoryId = pFoodTable.CategoryId;
                    foodTable.Title = pFoodTable.Title;
                    foodTable.Price = pFoodTable.Price;
                    foodTable.Picture = pFoodTable.Picture;
                    dbContexto.Update(foodTable);
                    result = await dbContexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error interno");
            }
            return result;
        }

        public static async Task<int> DeleteAsync(foodTable pFoodTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    var foodTable = await dbContexto.foodTable.FirstOrDefaultAsync(s => s.Id == pFoodTable.Id);
                    dbContexto.foodTable.Remove(foodTable);
                    result = await dbContexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("Ocurrio un error interno", ex);
            }
            return result;
        }

        public static async Task<foodTable> ObtenerPorIdAsync(foodTable pFoodTable)
        {
            var foodTable = new foodTable();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    foodTable = await dbContexto.foodTable.FirstOrDefaultAsync(s => s.Id == pFoodTable.Id);
                }
            }
            catch (MySqlException ex)
            {

                throw new Exception($"Ocurrio un error interno  {ex}");
            }
            return foodTable;
        }

        public static async Task<List<foodTable>> ObtenerTodosAsync()
        {
            List<foodTable> foodTables = new List<foodTable>();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    foodTables = await dbContexto.foodTable
                        .OrderByDescending(s => s.Id)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error interno", ex);
            }
            return foodTables;
        }

        public static async Task<List<foodTable>> ObtenerTodosPorCategoriasAsync(categoryTable pCategoryTable)
        {
            List<foodTable> food = new List<foodTable>();
            try
            {
                using (var dbContext = new DBContexto())
                {
                    food = await dbContext.foodTable
                        .Where(food => food.CategoryId == pCategoryTable.Id)
                        .ToListAsync();
                }
                return food;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error {ex.Message}");
                
            }
        }


        internal static IQueryable<foodTable> QuerySelect(IQueryable<foodTable> pQuery, foodTable pFoodTable)
        {
            if (pFoodTable.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pFoodTable.Id);
            if (pFoodTable.CategoryId > 0)
                pQuery = pQuery.Where(s => s.CategoryId == pFoodTable.CategoryId);
            if (!string.IsNullOrWhiteSpace(pFoodTable.Title))
                pQuery = pQuery.Where(s => s.Title.Contains(pFoodTable.Title));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            return pQuery;
        }

        public static async Task<List<foodTable>> BuscarAsync(foodTable pFoodTable)
        {
            var foodTable = new List<foodTable>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.foodTable.AsQueryable();
                select = QuerySelect(select, pFoodTable);
                foodTable = await select.ToListAsync();
            }

            return foodTable;
        }
    }
}
