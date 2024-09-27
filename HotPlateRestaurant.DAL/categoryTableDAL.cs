using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;

namespace HotPlateRestaurant.DAL
{
    public class categoryTableDAL
    {
        public static async Task<int> CrearAsync(categoryTable pCategoryTable)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                dbContexto.Add(pCategoryTable);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(categoryTable pCategoryTable)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var categorytable = await dbContexto.categoryTable.FirstOrDefaultAsync(s => s.Id == pCategoryTable.Id);
                categorytable.Name = pCategoryTable.Name;
                dbContexto.Update(categorytable);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> DeleteAsync(categoryTable pCategoryTable)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var categorytable = await dbContexto.categoryTable.FirstOrDefaultAsync(x => x.Id == pCategoryTable.Id);
                //Borrar por medio de cambio de estado
                //rol.Top_Aux = 2;//Se coloca el campo de Estado que coloco en su clases
                //dbContexto.Update(rol);
                //result = await dbContexto.SaveChangesAsync();
                //fin de codigo para eliminar por cambio de estado
                dbContexto.categoryTable.Remove(categorytable);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<categoryTable> ObtenerPorIdAsync(categoryTable pCategoryTable)
        {
            categoryTable categorytable = new categoryTable();
            using (var dbContexto = new DBContexto())
            {
                categorytable = await dbContexto.categoryTable.FirstOrDefaultAsync(s => s.Id == pCategoryTable.Id);
            }
            return categorytable;
        }

        public static async Task<List<categoryTable>> ObtenerTodosAsync()
        {
            List<categoryTable> categoriestable = new List<categoryTable>();
            using (var dbContexto = new DBContexto())
            {
                categoriestable = await dbContexto.categoryTable
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            return categoriestable;
        }
        /*
         * 
         */
        internal static IQueryable<categoryTable> QuerySelect(IQueryable<categoryTable> pQuery, categoryTable pCategoryTable)
        {
            if (pCategoryTable.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pCategoryTable.Id);
            if (!string.IsNullOrWhiteSpace(pCategoryTable.Name))
                pQuery = pQuery.Where(s => s.Name.Contains(pCategoryTable.Name));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCategoryTable.Top_Aux > 0)
                pQuery = pQuery.Take(pCategoryTable.Top_Aux).AsQueryable();
            return pQuery;
        }
        /// <summary>
        /// Victor Duran
        /// 19/02/2024
        /// Este metodo se ocupa para hacer la busqueda de un rol 
        /// por medio de condociones 
        /// </summary>
        /// <param name="pRol">Parametro con los datos a buscar</param>
        /// <returns></returns>
        public static async Task<List<categoryTable>> BuscarAsync(categoryTable pCategoryTable)
        {
            var categoriestable = new List<categoryTable>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.categoryTable.AsQueryable();
                select = QuerySelect(select, pCategoryTable);
                categoriestable = await select.ToListAsync();
            }
            return categoriestable;
        }
    }
}
