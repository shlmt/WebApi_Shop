using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
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
        private IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

/*        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User user = await _usersService.getUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
*/
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUser loginUser)
        {
            User user = await _usersService.checkLogin(loginUser.Email,loginUser.Password);
            if (user == null)
                return NoContent();
            else
                Response.Cookies.Append("X-Access-Token", user.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO user)
        {
            User user2 = _mapper.Map<UserDTO, User>(user);
            User u = await _usersService.createUser(user2);
            if (u == null)
                return BadRequest();
            return Ok(CreatedAtAction(nameof(u),new {id = user2.Id}, user));
        }

        [HttpPost]
        [Route("passStrength")]
        public int CheckPassStrength([FromBody] string pass)
        {
            return _usersService.CheckPasswordStregth(pass);
        }

        [HttpPut]
        [Route("update/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Update(int id,[FromBody] UserDTO newUser)
        {
            User user = _mapper.Map<UserDTO, User>(newUser);
            User u = await _usersService.updateUser(id,user);
            return Ok(u);
        }
    }
}
