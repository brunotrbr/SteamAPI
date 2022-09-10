using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamAPI.AuthorizationAndAuthentication;
using SteamAPI.Dto;
using SteamAPI.Interfaces;
using SteamAPI.Models;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;
        private readonly GenerateToken _generateToken;

        public UsersController(IUsersRepository repository, GenerateToken generateToken)
        {
            _repository = repository;
            _generateToken = generateToken;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, int maxResults)
        {
            var users = await _repository.Get(page, maxResults);
            return Ok(users);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertUser([FromBody] UsersDto usersDto)
        {
            if(!User.IsInRole("Manager")){
                return Forbid();
            }

            var user = new Users();
            user.Name = usersDto.Name;
            user.Username = usersDto.Username;
            user.Password = usersDto.Password;
            user.Role = usersDto.Role;
            var created = await _repository.Insert(user);
            return Created("",created);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Authenticate authInfo)
        {
            var user = await _repository.Get(authInfo.Username, authInfo.Password);
            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha Inválidos" });
            }

            var token = _generateToken.GenerateJwt(user);
            user.Password = "";
            return Ok(new { user = user, token = token });
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Guest,Junior,Manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Gerente";
    }
}
