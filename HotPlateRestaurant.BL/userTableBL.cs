using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;

namespace HotPlateRestaurant.BL
{
    public class userTableBL
    {
        public async Task<int> CrearAsync(userTable pUserTable)
        {
            return await userTableDAL.GuardarAsync(pUserTable);
        }

        public async Task<int> ModificarAsync(userTable pUserTable)
        {
            return await userTableDAL.ModificarAsync(pUserTable);
        }

        public async Task<int> DeleteAsync(userTable pUserTable)
        {
            return await userTableDAL.EliminarAsync(pUserTable);
        }

        public async Task<userTable> ObtenerPorIdAsync(userTable pUserTable)
        {
            return await userTableDAL.ObtenerPorIdAsync(pUserTable);
        }

        public async Task<List<userTable>> ObtenerTodosAsync()
        {
            return await userTableDAL.ObtenerTodosAsync();
        }

        public async Task<List<userTable>> BuscarAsync(userTable pUserTable)
        {
            return await userTableDAL.BuscarAsync(pUserTable);
        }

        public async Task<userTable> LoginAsync(userTable pUserTable)
        {
            return await userTableDAL.LoginAsync(pUserTable);
        }

        public async Task<int> CambiarPasswordAsync(userTable pUserTable,
           string pPasswordAnt)
        {
            return await userTableDAL.CambiarPasswordAsync(pUserTable, pPasswordAnt);
        }
    }
}
