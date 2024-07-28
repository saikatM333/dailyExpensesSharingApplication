using Microsoft.AspNetCore.Mvc;
using modelLayer.model;
using repositoryLayer.entity;
using repositoryLayer.Interface;
using System.Threading.Tasks;


namespace DailyExpensesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                MobileNumber = userDto.MobileNumber
            };
            try
            {
                var createdUser = await _userService.CreateUserAsync(user);
                return Ok(createdUser);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
