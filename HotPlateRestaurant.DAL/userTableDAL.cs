using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HotPlateRestaurant.EN;
using Microsoft.EntityFrameworkCore;
using static HotPlateRestaurant.EN.userTable;

namespace HotPlateRestaurant.DAL
{
    public class userTableDAL
    {
        private static void EncriptarMD5(userTable pUserTable)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(
                    pUserTable.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUserTable.Password = strEncriptar;
            }
        }
        private static async Task<bool> ExisteLogin(userTable pUserTable,
           DBContexto pDbContexto)
        {
            bool result = false;
            var loginUserExists = await pDbContexto.userTable.
                FirstOrDefaultAsync(a => a.Email == pUserTable.Email &&
                a.Id != pUserTable.Id);
            if (loginUserExists != null && loginUserExists.Id > 0 &&
                loginUserExists.Email == pUserTable.Email)
                result = true;
            return result;
        }
        #region "CRUD"
        public static async Task<int> GuardarAsync(userTable pUserTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    bool loginUserExists = await ExisteLogin(pUserTable, dbContexto);
                    if (loginUserExists == false)
                    {
                        pUserTable.CreatedAt = DateTime.Now;
                        EncriptarMD5(pUserTable);
                        dbContexto.Add(pUserTable);
                        result = await dbContexto.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Login already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("An internal error occurred");
            }
            return result;
        }
        public static async Task<int> ModificarAsync(userTable pUserTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    bool loginUserExists = await ExisteLogin(pUserTable, dbContexto);
                    if (loginUserExists == false)
                    {
                        var user = await dbContexto.userTable.FirstOrDefaultAsync(
                            d => d.Id == pUserTable.Id);
                        user.Name = pUserTable.Name;
                        user.LastName = pUserTable.LastName;
                        user.Email = pUserTable.Email;
                        dbContexto.Update(user);
                        result = await dbContexto.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Login already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("An internal error ocurred");
            }
            return result;
        }
        public static async Task<int> EliminarAsync(userTable pUserTable)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    var user = await dbContexto.userTable.FirstOrDefaultAsync(
                        f => f.Id == pUserTable.Id);
                    dbContexto.userTable.Remove(user);
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
        public static async Task<userTable> ObtenerPorIdAsync(userTable pUserTable)
        {
            var user = new userTable();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    user = await dbContexto.userTable.FirstOrDefaultAsync(
                        s => s.Id == pUserTable.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An internal error ocurred");
            }
            return user;
        }
        public static async Task<List<userTable>> ObtenerTodosAsync()
        {
            List<userTable> users = new List<userTable>();
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    users = await dbContexto.userTable
                        .OrderByDescending(s => s.Id)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An internal error ocurred");
            }
            return users;
        }
        internal static IQueryable<userTable> QuerySelect(IQueryable<userTable> pQuery,
            userTable pUserTable)
        {
            if (pUserTable.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pUserTable.Id);
            if (!string.IsNullOrWhiteSpace(pUserTable.Name))
                pQuery = pQuery.Where(s => s.Name.Contains(pUserTable.Name));
            if (!string.IsNullOrWhiteSpace(pUserTable.LastName))
                pQuery = pQuery.Where(s => s.LastName.Contains(pUserTable.LastName));
            if (!string.IsNullOrWhiteSpace(pUserTable.Email))
                pQuery = pQuery.Where(s => s.Email == pUserTable.Email);
            if (pUserTable.CreatedAt.Year > 1000)
            {
                DateTime initialDate = new DateTime(pUserTable.CreatedAt.Year,
                    pUserTable.CreatedAt.Month, pUserTable.CreatedAt.Day, 0, 0, 0);
                DateTime fechaFinal = initialDate.AddDays(-3).AddMilliseconds(1);
                pQuery = pQuery.Where(s => s.CreatedAt >= initialDate &&
                s.CreatedAt <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pUserTable.Top_Aux > 0)
                pQuery = pQuery.Take(pUserTable.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<userTable>> BuscarAsync(userTable pUserTable)
        {
            var users = new List<userTable>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.userTable.AsQueryable();
                select = QuerySelect(select, pUserTable);
                users = await select.ToListAsync();
            }
            return users;
        }
        #endregion

        public static async Task<userTable> LoginAsync(userTable pUserTable)
        {
            var user = new userTable();
            using (var dbContexto = new DBContexto())
            {
                EncriptarMD5(pUserTable);
                user = await dbContexto.userTable.FirstOrDefaultAsync(s =>
                s.Email == pUserTable.Email && s.Password == pUserTable.Password &&
                s.Status == (byte)Estatus_Usuario.ACTIVE);
            }
            return user;
        }
        public static async Task<int> CambiarPasswordAsync(userTable pUserTable,
            string pPasswordAnt)
        {
            int result = 0;
            var usuarioPassAnt = new userTable { Password = pPasswordAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var dbContexto = new DBContexto())
            {
                var usuario = await dbContexto.userTable.FirstOrDefaultAsync(
                    s => s.Id == pUserTable.Id);
                if (usuarioPassAnt.Password == usuario.Password)
                {
                    EncriptarMD5(pUserTable);
                    usuario.Password = pUserTable.Password;
                    dbContexto.Update(usuario);
                    result = await dbContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("The current password is incorrect");
            }
            return result;
        }

    }
}
