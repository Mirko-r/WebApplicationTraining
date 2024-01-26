using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [Route("/api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private DataContext _context;

        public LoginController(IConfiguration config, DataContext ctx) { _config = config; _context = ctx; }

        [HttpPost]
        [AllowAnonymous] // non c'è bisogno di presentare un token
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized(); // 401
            var user = AuthenticateUser(login);
            if (user != null)
            {
                string tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private User AuthenticateUser(UserModel login)
        {
            User u = _context.Users.Where<User>(u => u.Username.Equals(login.Username) && u.Password.Equals(login.Password)).First();
            // db access to check credentials
            //foreach (User user in _context.Users)
            //{
            //    if (user.Username.Equals(login.Username) && user.Password.Equals(login.Password))
            //    {
            //        return user;
            //    }
            //}
            return u;
        }

        private string GenerateJSONWebToken(User login)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, login.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // uid univoco del claim 
                new Claim("extraClaim", "i'm using c#"),
                new Claim("role", login.Role.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            //check for available claims
            var currentUser = HttpContext.User;

            foreach (var c in currentUser.Claims)
            {
                if (c.Type.Contains("role"))
                {
                    if (c.Value.Contains("ADMIN") && c.Value.Contains("DEV"))
                    {
                        Console.WriteLine(c.Value);
                        return Ok( new string[] { "valore1", "valore2" });
                    }
                }
            }
            return  Unauthorized( new string[] { "You are now allowed to invoke this method" });
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get2(long id)
        {
            var currentUser = HttpContext.User;

            foreach (var c in currentUser.Claims)
            {
                if (c.Type.Contains("role"))
                {
                    if (c.Value.Contains("PUBLIC"))
                    {
                        Console.WriteLine(c.Value);
                        return Ok(new string[] { "value1" + id });
                    }
                }
            }
            return Unauthorized(new string[] { "You are now allowed to invoke this method" });
        }
    }
}