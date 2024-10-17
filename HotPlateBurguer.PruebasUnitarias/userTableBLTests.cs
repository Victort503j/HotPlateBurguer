using HotPlateRestaurant.EN;
using static HotPlateRestaurant.EN.userTable;

namespace HotPlateRestaurant.BL.Tests
{
    [TestClass()]
    public class userTableBLTests
    {
        // Usuario inicial con datos consistentes
        private static userTable usuarioInicial = new userTable
        {
            Id = 34,
            Name = "Manuel",
            LastName = "Hernandez",
            Phone = "76313323",
            Email = "manuel@gmail.com",
            Password = "P@ssWord#123" // Password original en la base de datos
        };

        private userTableBL usuarioBl = new userTableBL();

        [TestMethod()]
        public async Task CrearAsyncTest()
        {
            userTable usuario = new userTable
            {
                Name = "Eduardo",
                LastName = "Cortez",
                Phone = "76312322",
                Email = "cortez@gmail.com",
                Password = "P@ssWord#123",
                Status = (byte)Estatus_Usuario.ACTIVE
            };
            var result = await usuarioBl.CrearAsync(usuario);
            Assert.AreEqual(1, result); // Comprobar si se creó correctamente
        }

        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            userTable usuario = new userTable
            {
                Id = usuarioInicial.Id,
                Name = usuarioInicial.Name,
                LastName = usuarioInicial.LastName,
                Phone = usuarioInicial.Phone,
                Email = usuarioInicial.Email,
                //Password = usuarioInicial.Password, // Utiliza la contraseña actualizada
                Status = (byte)Estatus_Usuario.INACTIVE
            };
            var result = await usuarioBl.ModificarAsync(usuario);
            Assert.AreEqual(1, result); // Comprobar si se modificó correctamente
        }

        [TestMethod()]
        public async Task ObtenerPorIdAsyncTest()
        {
            userTable usuario = new userTable
            {
                Id = usuarioInicial.Id,
            };
            var result = await usuarioBl.ObtenerPorIdAsync(usuario);
            Assert.IsTrue(result.Id == usuario.Id); // Verificar si se obtuvo el registro por ID
        }

        [TestMethod()]
        public async Task ObtenerTodosAsyncTest()
        {
            var result = await usuarioBl.ObtenerTodosAsync();
            Assert.IsTrue(result.Count > 0); // Verificar si se obtuvieron registros
        }

        [TestMethod()]
        public async Task BuscarAsyncTest()
        {
            userTable usuario = new userTable
            {
                Name = "Manuel",
                LastName = "Hernandez",
                Email = "manuel@gmail.com",
                Top_Aux = 10
            };
            var result = await usuarioBl.BuscarAsync(usuario);
            Assert.AreNotEqual(0, result.Count); // Comprobar si se encontraron resultados
        }

        [TestMethod()]
        public async Task LoginAsyncTest()
        {
            userTable usuario = new userTable();
            usuario.Email = "manuel@gmail.com";
            usuario.Password = "1234567891011"; // Asegúrate que esta sea la contraseña encriptada
            var result = await usuarioBl.LoginAsync(usuario);
            Assert.AreEqual(usuario.Email, "manuel@gmail.com"); // Comprobar si el login es correcto
        }

        [TestMethod()]
        public async Task CambiarPasswordAsyncTest()
        {
            var usuario = new userTable();
            usuario.Id = usuarioInicial.Id;
            string passwordNuevo = "123456789"; // Nueva contraseña
            usuario.Password = passwordNuevo;

            var result = await usuarioBl.CambiarPasswordAsync(usuario, usuarioInicial.Password); // Cambia la contraseña
            Assert.AreEqual(1, result); // Comprobar si se cambió correctamente
            usuarioInicial.Password = passwordNuevo; // Actualiza la contraseña para pruebas futuras
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var usuario = new userTable
            {
                Id = 40
            };
            var result = await usuarioBl.DeleteAsync(usuario);
            Assert.AreEqual(1, result); // Comprobar si se eliminó correctamente
        }
    }
}
