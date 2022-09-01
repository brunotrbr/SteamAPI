using Microsoft.AspNetCore.Mvc;
using SteamAPI.Dto;
using SteamAPI.Filters;
using SteamAPI.Interfaces;
using SteamAPI.Models;
using SteamAPI.Repositories;
using System.Net.Mime;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAsyncActionFilterController]
    public class GamesController : ControllerBase
    {

        private readonly IBaseRepository<Games> _repository;

        public GamesController(IBaseRepository<Games> repository)
        {
            _repository = repository;
        }

        private Games UpdateGamesModel(Games newData, GamesDto entity)
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
        [CustomActionFilterEndpoint]
        public async Task<IActionResult> Get([FromQuery] int page, int maxResults)
        {
            var games = await _repository.Get(page, maxResults);
            return Ok(games);
        }

        [HttpGet("{id}")]
        [ShortCircuitFilter]
        [ProducesResponseType(typeof(Games), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var game = await _repository.GetByKey(id);
            if (game == null)
            {
                return NotFound("Id Inexistente");
            }
            return Ok(game);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json, new[] { "application/xml", "text/plain" })]
        [ProducesResponseType(typeof(Games), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Games), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GamesDto entity)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                var gameToInsert = new Games(id: 0, entity.AppId, entity.Name, entity.Developer, entity.Platforms, genres: entity.Genres, categories: entity.Categories); // DAO
                var inserted = await _repository.Insert(gameToInsert);
                return Created(string.Empty, inserted);
            }

            databaseGames = UpdateGamesModel(databaseGames, entity);

            var updated = await _repository.Update(id, databaseGames);

            return Ok(updated);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GamesDto entity)
        {
            var gameToInsert = new Games(id: 0, entity.AppId, entity.Name, entity.Developer, entity.Platforms, genres: entity.Genres, categories: entity.Categories); // DAO
            var inserted = await _repository.Insert(gameToInsert);
            return Created(string.Empty, inserted);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] GamesPatchDto entity)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                return NoContent();
            }

            databaseGames.Platforms = entity.Platforms;

            var updated = await _repository.Update(id, databaseGames);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var databaseGames = await _repository.GetByKey(id);

            if (databaseGames == null)
            {
                return NoContent();
            }

            var deleted = await _repository.Delete(id);
            return Ok(deleted);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> RecuperaDados([FromRoute] int id)
        {
            var game = await _repository.GetByKey(id);
            if (game == null)
            {
                return NotFound("Id Inexistente");
            }
            return Ok(game);
        }
    }
}
