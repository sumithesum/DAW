using DAW.Interfaces;
using DAW.Modells;
using DAW.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using AutoMapper;
using Daw.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Daw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IgameInterface _gameInterface;
        private readonly IMapper _mapper;
        public GameController(IgameInterface gameInterface , IMapper mapper)
        {
            _mapper = mapper;
            _gameInterface = gameInterface;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]

        public IActionResult GetGames()
        {


            var games = _mapper.Map<List<GameDto>>( _gameInterface.GetGames());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(games);
        }


        [HttpGet("{gameId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]

        public IActionResult GetGame(int gameId) { 
        
            if (!_gameInterface.GameExists(gameId))
                return NotFound();
            
            var game = _mapper.Map<GameDto>( _gameInterface.GetGame(gameId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(game);
        }

        [HttpGet("{gameid}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public  IActionResult GetGameRating(int gameId)
        {
            if (!_gameInterface.GameExists(gameId))
                return NotFound();

            var rating = _gameInterface.GetScore(gameId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CreateGame([FromBody] GameDto gameCreate) {
            
            if (gameCreate == null)
                return BadRequest(ModelState);

            var game = _gameInterface.GetGames().Where(c => c.Name.ToLower() == gameCreate.Name.TrimEnd().ToLower())
                .FirstOrDefault();

            if(game != null)
            {
                ModelState.AddModelError("", "E deja un joc asa");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var gamemap = _mapper.Map<Game>(gameCreate);

            if (!_gameInterface.CreateGame(gamemap)){
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Succes");
        }

        [HttpPut, Authorize(Roles = "Admin")]
        [ProducesResponseType(400)][ProducesResponseType(204)][ProducesResponseType(404)]

        public IActionResult UpdateGame(int gameid, [FromBody] GameDto game) { 
            
            if(game == null) return BadRequest(ModelState);

            if(gameid != game.ID)
                return BadRequest(ModelState);

            if (!_gameInterface.GameExists(gameid))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var gamemap = _mapper.Map<Game>(game);

            if (!_gameInterface.UpdateGame(gamemap))
            {
                ModelState.AddModelError("","IDK CEVA LA UPDATE");
                return StatusCode(500,ModelState);
            }

            return Ok("Suces");
        }

        [HttpDelete("{gameid}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)][ProducesResponseType(204)][ProducesResponseType(404)]

        public IActionResult DeleteGame(Game game,int gameid)
        {
            if(!_gameInterface.GameExists(game.ID)) { return NotFound(); }

            var gametodel = _gameInterface.GetGame(gameid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_gameInterface.DeleteGame(gametodel))
            {
                ModelState.AddModelError("", "Nu am mers deletul frate");
                return StatusCode(500,ModelState);
            }

            return Ok("Succes");


        }
    }
}
