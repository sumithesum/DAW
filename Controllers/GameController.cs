using DAW.Interfaces;
using DAW.Modells;
using DAW.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using AutoMapper;
using Daw.DTO;

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


        [HttpGet("{gameId}")]
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
    }
}
