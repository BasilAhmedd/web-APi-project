using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shit.Repo;

namespace shit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;

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
    }
}
