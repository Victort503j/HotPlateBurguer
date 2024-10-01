using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HotPlateRestaurant.BL;
using HotPlateRestaurant.DAL;
using HotPlateRestaurant.EN;
using HotPlateRestaurantAPI.Model;
using HotPlateRestaurantAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotPlateRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private userTableBL _userServiceBl = new userTableBL();  // Un servicio para manejar el registro y validación de usuarios

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Acción para manejar el login y generar el token JWT
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] object pUser)
        {
            // Validamos las credenciales del usuario a través del servicio de usuarios
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string strUserTable = JsonSerializer.Serialize(pUser);
            userTable user = JsonSerializer.Deserialize<userTable>(strUserTable, option);
            userTable userResult = await _userServiceBl.ExisteLoginUser(user);

            if (userResult == null)
            {
                return Unauthorized();
            }
            else
            {
                var jwtSettings = _configuration.GetSection("JwtSetting");
                var secretKey = jwtSettings.GetValue<string>("SecretKey");
                var issuer = jwtSettings.GetValue<string>("Issuer");
                var audience = jwtSettings.GetValue<string>("Audience");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { 
                        new Claim(ClaimTypes.Name, userResult.Name),
                        //new Claim(ClaimTypes.Role, user.roles)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                //Usando parametro definido
                var token = tokenHandler.CreateToken(tokenDescriptor);
                //Convertir token a una cadena
                var jwt = tokenHandler.WriteToken(token);

                return Ok(jwt);

            }
        }

        // Acción para manejar el registro de nuevos usuarios
        //[HttpPost("register")]
        //public IActionResult Register([FromBody] RegisterRequest registerRequest)
        //{
        //    // Registramos al nuevo usuario usando el servicio de usuarios
        //    var result = _userService.RegisterUser(registerRequest.Username, registerRequest.Password);

        //    if (result)
        //    {
        //        return Ok(new { Message = "User registered successfully" });
        //    }

        //    return BadRequest(new { Message = "User registration failed" });
        //}
    }
}
