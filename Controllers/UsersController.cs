using Microsoft.AspNetCore.Mvc;
using SteamAPI.Dto;
using SteamAPI.Interfaces;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;

        public UsersController(IUsersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, int maxResults)
        {
            var users = await _repository.Get(page, maxResults);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserByUserPass([FromBody] UsersDto usersDto)
        {
            var user = await _repository.Get(usersDto.Username, usersDto.Password);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
