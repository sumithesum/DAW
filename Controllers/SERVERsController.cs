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
using System.Diagnostics.Metrics;
using Daw.Repository;
using Daw.Interfaces;
using Daw.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Daw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERVERsController : Controller
    {
       
        private readonly SERVERInterface _serverRepository;
        private readonly IMapper _mapper;
        public SERVERsController(SERVERInterface SERVERInterface, IMapper mapper)
        {
            _serverRepository = SERVERInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SERVER>))]
        public IActionResult Getservers()
        {
            var servers = _mapper.Map<List<ServerDto>>(_serverRepository.GetServers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(servers);
        }

        [HttpGet("{SERVERId}")]
        [ProducesResponseType(200, Type = typeof(SERVER))]
        [ProducesResponseType(400)]
        public IActionResult GetSERVER(int SERVERId)
        {
            if (!_serverRepository.SERVERExists(SERVERId))
                return NotFound();

            var SERVER = _mapper.Map<ServerDto>(_serverRepository.GetSERVER(SERVERId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(SERVER);
        }

        [HttpGet("/players/{playerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(SERVER))]
        public IActionResult GetSERVEROfAnplayer(int playerId)
        {
            var SERVER = _mapper.Map<ServerDto>(
                _serverRepository.GetSERVERByPlayer(playerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(SERVER);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSERVER([FromBody] ServerDto SERVERCreate)
        {
            if (SERVERCreate == null)
                return BadRequest(ModelState);

            var SERVER = _serverRepository.GetServers()
                .Where(c => c.Region.Trim().ToUpper() == SERVERCreate.Region.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (SERVER != null)
            {
                ModelState.AddModelError("", "SERVER already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SERVERMap = _mapper.Map<SERVER>(SERVERCreate);

            if (!_serverRepository.CreateSERVER(SERVERMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{SERVERId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int SERVERId, [FromBody] ServerDto updatedSERVER)
        {
            if (updatedSERVER == null)
                return BadRequest(ModelState);

            if (SERVERId != updatedSERVER.ID)
                return BadRequest(ModelState);

            if (!_serverRepository.SERVERExists(SERVERId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var SERVERMap = _mapper.Map<SERVER>(updatedSERVER);

            if (!_serverRepository.UpdateSERVER(SERVERMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{SERVERId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSERVER(int SERVERId)
        {
            if (!_serverRepository.SERVERExists(SERVERId))
            {
                return NotFound();
            }

            var SERVERToDelete = _serverRepository.GetSERVER(SERVERId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_serverRepository.DeleteSERVER(SERVERToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }


    }
}
