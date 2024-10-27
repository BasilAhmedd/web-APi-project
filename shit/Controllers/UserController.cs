using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using shit.Repo;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace shit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepo userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }
        [HttpGet("{id}")]
        public IActionResult Get ([FromRoute] User user) {
            if (User != null)
            {
               var users = _userRepo.GetUserById(user.Id);
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Post ([FromBody]UserDTO dto) {

            _userRepo.AddUser(dto);
            return Ok(dto);
        } 
        [HttpPut]
        public IActionResult Put ([FromBody]UserDTO dto ,[FromRoute] int id) {
            _userRepo.UpdateUser(dto);
            return Ok(dto);
            //var user = _userRepo.GetUserById(id);
            //if (user != null)
            //{
            //    user.Email = dto.Email;
            //    user.Name = dto.Name;
            //    _userRepo.SaveChanges();
            //    return Ok(user);
            //}

        }

        [HttpDelete]
        public IActionResult Delete ([FromRoute]int id)
        {
            _userRepo?.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("Login")]
        public IActionResult ValidateUser([FromBody] UserDTO userDTO)
        {

            var user = _userRepo.ValidateUser(userDTO.Email,userDTO.Password);
            if (user == null)
            {
                return Unauthorized("Email or password wrong");
            }
            var token = GenerateJwtToken(user);
            return Ok(token);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
