using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAW.Data;
using DAW.Modells;
using AutoMapper;
using Daw.Interfaces;
using Daw.DTO;

namespace Daw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly PlayerInterface _playerInterface;
        private readonly SERVERInterface _serverInterface;
        private readonly IMapper _mapper;

        public PlayerController(PlayerInterface playerInterface,
            SERVERInterface serverInterface,
            IMapper mapper)
        {
            _playerInterface = playerInterface;
            _serverInterface = serverInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
        public IActionResult GetPlayers()
        {
            var Players = _mapper.Map<List<PlayerDto>>(_playerInterface.GetPlayers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Players);
        }

        [HttpGet("{PlayerId}")]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public IActionResult GetPlayer(int PlayerId)
        {
            if (!_playerInterface.PlayerExists(PlayerId))
                return NotFound();

            var Player = _mapper.Map<PlayerDto>(_playerInterface.GetPlayer(PlayerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Player);
        }

        [HttpGet("{PlayerId}/Game")]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public IActionResult GetGameByPlayer(int PlayerId)
        {
            if (!_playerInterface.PlayerExists(PlayerId))
            {
                return NotFound();
            }

            var Player = _mapper.Map<List<GameDto>>(
                _playerInterface.GetGameByPlayer(PlayerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Player);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlayer([FromQuery] int ServerId, [FromBody] PlayerDto PlayerCreate)
        {
            if (PlayerCreate == null)
                return BadRequest(ModelState);

            var Players = _playerInterface.GetPlayers()
                .Where(c => c.Name.Trim().ToUpper() == PlayerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Players != null)
            {
                ModelState.AddModelError("", "Player already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PlayerMap = _mapper.Map<Player>(PlayerCreate);

            PlayerMap.Server = _serverInterface.GetSERVER(ServerId);

            if (!_playerInterface.CreatePlayer(PlayerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{PlayerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePlayer(int PlayerId, [FromBody] PlayerDto updatedPlayer)
        {
            if (updatedPlayer == null)
                return BadRequest(ModelState);

            if (PlayerId != updatedPlayer.ID)
                return BadRequest(ModelState);

            if (!_playerInterface.PlayerExists(PlayerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var PlayerMap = _mapper.Map<Player>(updatedPlayer);

            if (!_playerInterface.UpdatePlayer(PlayerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Player");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{PlayerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlayer(int PlayerId)
        {
            if (!_playerInterface.PlayerExists(PlayerId))
            {
                return NotFound();
            }

            var PlayerToDelete = _playerInterface.GetPlayer(PlayerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_playerInterface.DeletePlayer(PlayerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Player");
            }

            return NoContent();
        }
    }
}
