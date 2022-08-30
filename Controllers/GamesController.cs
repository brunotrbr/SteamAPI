using Microsoft.AspNetCore.Mvc;
using SteamAPI.Interfaces;
using SteamAPI.Models;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IBaseRepository<Games> _repository;

        public GamesController(ILogger<GamesController> logger, IBaseRepository<Games> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private Games UpdateGamesModel(Games newData, Games entity)
        {
            newData.AppId = entity.AppId;
            newData.Name = entity.Name;
            newData.Developer = entity.Developer;
            newData.Platforms = entity.Platforms;
            newData.Categories = entity.Categories;
            newData.Genres = entity.Genres;
            return newData;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var games = await _repository.Get();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var games = await _repository.GetByKey(id);
            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Games entity)
        {
            var inserted = await _repository.Insert(entity);
            return Created(String.Empty, inserted);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Games entity)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                var inserted = await _repository.Insert(entity);
                return Created(String.Empty, inserted);
            }

            databaseGames = UpdateGamesModel(databaseGames, entity);

            var updated = await _repository.Update(id, databaseGames);

            return Ok(updated);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, [FromBody] Games entity)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                var error = "Id inexistente.";
                return NotFound(error);
            }

            databaseGames = UpdateGamesModel(databaseGames, entity);

            var updated = await _repository.Update(id, databaseGames);

            return Ok(updated);
        }

        // DELETE <GamesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                var error = "Id inexistente.";
                return NotFound(error);
            }

            var deleted = await _repository.Delete(id);

            return Ok(deleted);
        }
    }
}
