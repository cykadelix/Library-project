using Library_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library_project.Controllers
{
    public class logincontroller : Controller
    {
        IConfiguration _config;
        public logincontroller(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult LoginPage()
        {
            userdto formUser= new userdto();
            return View(formUser);
        }
        public async Task<IActionResult> LoginLandingPage(userdto formUser)
        {
            user user = new user();

            if(ModelState.IsValid)
            {
                await using NpgsqlConnection conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true");
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("SELECT * FROM userdto WHERE @username=username AND @password=password",conn)
                {
                    Parameters=
                    {
                        new("username", formUser.UserName),
                        new("password",formUser.password)
                    }


                };
                NpgsqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    user.username = reader.GetString(0);
                    user.password = reader.GetString(1);
                    user.id = reader.GetInt32(2);
                    user.role = reader.GetString(3);
 
                    string token = CreateToken(user);
                    ViewBag.message = "success";
                    ViewBag.token = token;

                }
                else 
                {
                    ViewBag.message = "failure";
                }
                
                
        
            }
            else
            {
                ViewBag.message = "invalide state";
            }

            return View(user);


        }
        private string CreateToken(user user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var Token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwt;
        }
            
      }
}
       
