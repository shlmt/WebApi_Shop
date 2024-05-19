using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.Json;

namespace project.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User user = await _usersService.getUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUser loginUser)
        {
            Console.WriteLine(loginUser.ToString());
            User user = await _usersService.checkLogin(loginUser);
            Console.WriteLine(user.ToString());
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            User u = await _usersService.createUser(user);
            return CreatedAtAction(nameof(u),new {id = user.Id}, user);
        }

        [HttpPost]
        [Route("passStrength")]
        public int checkPassStrength([FromBody]Password pass)
        {
            return _usersService.CheckPasswordStregth(pass.Pass);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<User>> Update(int id,[FromBody] User newUser)
        {
            Console.WriteLine(newUser.ToString());
            Console.WriteLine(newUser.ToString());
            User u =await _usersService.updateUser(id,newUser);
            return Ok(u);
        }

    }
}
