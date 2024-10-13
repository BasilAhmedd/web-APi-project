using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace shit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context) {
          _context = context;

        }

        [HttpGet("{id}")]
        public IActionResult Get (int id) {
            if (User != null)
            {
               var user = _context.Users.FirstOrDefault(x => x.Id == id);
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Post (UserDTO dto) {
            var u = new User
            {
                Name = dto.Name,
                Email = dto.Email,
            };
            _context.Users.Add(u);
            _context.SaveChanges();
            return Ok(u);
        } 
        [HttpPut]
        public IActionResult Put (UserDTO dto , int id) {
            var user = _context.Users.FirstOrDefault (x => x.Id == id); 
            if(user != null)
            {
                user.Email = dto.Email;
                user.Name = dto.Name;
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
