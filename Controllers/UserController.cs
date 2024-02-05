using AutoMapper;
using Daw.DTO;
using Daw.Interfaces;
using Daw.Modells;
using DAW.Interfaces;
using DAW.Modells;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Daw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _congif;
        private readonly UserInterface _userInterface;
        private readonly IMapper _mapper;
     

        public UserController(UserInterface userInterface, IMapper mapper, IConfiguration congif)
        {
            _congif = congif;
            mapper = mapper;
            _userInterface = userInterface;
        }

        [HttpGet, Authorize(Roles = "noob")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]

        public IActionResult Getusers()
        {


            var users = _userInterface.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto rq)
        {
            CreatePasswordHash(rq.Password, out byte[] passwarodhash, out byte[] passwordsalt);
            user.UserName = rq.UserName;
            user.PasswordHash = passwarodhash;
            user.PasswordSalt = passwordsalt;   

            return Ok(user);

        }
        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(UserDto rq)
        {
            if (user.UserName != rq.UserName)
            {
                return BadRequest("UserNotFound");
            }
            if (!Verify(rq.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Incorect Password");

            string token = CreateToken(rq);
            return Ok(token);
        }
        

        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "noob")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_congif.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password,out byte[] passwarodhash ,out byte[] passwordsalt) {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwarodhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool Verify(string password,  byte[] passwarodhash,  byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var cmpHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return cmpHash.SequenceEqual(passwarodhash);
            }
        }
    }
}
