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
        private readonly IConfiguration _congif;
        private readonly UserInterface _userInterface;
        private readonly IMapper _mapper;
     

        public UserController(UserInterface userInterface, IMapper mapper, IConfiguration congif)
        {
            _congif = congif;
            mapper = mapper;
            _userInterface = userInterface;
        }

        [HttpGet, Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult Createuser([FromBody] UserDto userCreate)
        {

            if (userCreate == null)
                return BadRequest(ModelState);

            var user = _userInterface.GetUsers().Where(c => c.UserName.ToLower() == userCreate.UserName.TrimEnd().ToLower())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "E deja un user asa");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             user = new User();
            CreatePasswordHash(userCreate.Password, out byte[] passwarodhash, out byte[] passwordsalt);
            user.UserName = userCreate.UserName;
            user.PasswordHash = passwarodhash;
            user.PasswordSalt = passwordsalt;
            user.IsAdmin = false;
            if (!_userInterface.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Succes");
        }

        [HttpPut("ToAdmin"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateuserAdmin(int userid)
        {


            

            if (!_userInterface.UserExists(userid))
            {
                return NotFound();
            }

           User user = _userInterface.GetUser(userid);
            user.IsAdmin = true;
            if (!_userInterface.UpdateUser(user))
            {
                ModelState.AddModelError("", "IDK CEVA LA UPDATE");
                return StatusCode(500, ModelState);
            }

            return Ok("Suces");
        }

        [HttpPut("ToUser"),Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateuserUser(int userid)
        {




            if (!_userInterface.UserExists(userid))
            {
                return NotFound();
            }

            User user = _userInterface.GetUser(userid);
            user.IsAdmin = false;
            if (!_userInterface.UpdateUser(user))
            {
                ModelState.AddModelError("", "IDK CEVA LA UPDATE");
                return StatusCode(500, ModelState);
            }

            return Ok("Suces");
        }

        [HttpDelete("{userid}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult Deleteuser( int userid)
        {
            if (!_userInterface.UserExists(userid)) { return NotFound(); }

            var usertodel = _userInterface.GetUser(userid);

            

            if (!_userInterface.DeleteUser(usertodel))
            {
                ModelState.AddModelError("", "Nu am mers deletul frate");
                return StatusCode(500, ModelState);
            }

            return Ok("Succes");


        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(UserDto rq)
        {
            User user = _userInterface.GetUser(rq.Id);

            if (user == null) 
                return BadRequest("No user");
            if (user.UserName != rq.UserName)
                return BadRequest("UserNotFound");
            if (!Verify(rq.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Incorect Password");

            string token = string.Empty;
            if (user.IsAdmin)
                 token = CreateTokenAdmin(rq);
            else
                 token = CreateTokenUser(rq);
            return Ok(token);
        }
        

        private string CreateTokenAdmin(UserDto user)
        {
           
               List<Claim>  claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
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

        private string CreateTokenUser(UserDto user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };



            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_congif.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

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
