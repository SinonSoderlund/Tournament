using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Data.Repositories;
using Tournament.Core.Repositories;
using Tournament.Core.Dto;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork iUoW;
        private readonly IMapper mapper;
        public GamesController(IUnitOfWork iUoW, IMapper mapper)
        {
            this.iUoW = iUoW;
            this.mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameIdDto>>> GetGame()
        {
            return Ok(mapper.Map<IEnumerable<GameIdDto>>(await iUoW.GameRepository.GetAllAsync()));
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameIdDto>> GetGame(int? id, string? byName)
        {
            var game = await iUoW.GameRepository.GetAsync(id, byName);

            if (game == default)
            {
                return NotFound("Element does not exist");
            }

            return Ok(mapper.Map<GameIdDto>(game));
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDto game)
        {
            if (id != game.Id)
            {
                return BadRequest("Target Url Id does not match inserted object");
            }


            Game upd = await iUoW.GameRepository.GetAsync(id);

            if (upd == default)
                return NotFound("Requested object does not exist");

            TournamentDetails parent;

            if (game.TournamentId != upd.TournamentId)
            {
                parent = await iUoW.TournamentRepository.GetAsync(game.TournamentId);
                if (parent == default)
                    return NotFound("Requested parent object does not exist");
            }
            else
            {
                parent = upd.Tournament;
            }

            mapper.Map(game, upd);
            upd.Tournament = parent;

            TryValidateModel(upd);
            if (!ModelState.IsValid)
                BadRequest();

            try
            {
                iUoW.GameRepository.Update(upd);
                await iUoW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await iUoW.GameRepository.AnyAsync(id))
                {
                    return StatusCode(500);
                }
                else
                {
                    throw; 
                }
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, JsonPatchDocument<GameIdDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("Patch document cannot be null or empty");

            Game game = await iUoW.GameRepository.GetAsync(id);

            if (game == default)
                return NotFound("Requested object does not exist.");

            GameIdDto dto = mapper.Map<GameIdDto>(game);

            patchDocument.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                UnprocessableEntity(ModelState);

            mapper.Map(dto, game);
            if (!TryValidateModel(game))
                BadRequest("Invalid patch attempt.");

            iUoW.GameRepository.Update(game);
            await iUoW.CompleteAsync();

            return NoContent();
        }


        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameCreateDto game)
        {
            TournamentDetails parent;

            parent = await iUoW.TournamentRepository.GetAsync(game.TournamentId);
            if (parent == default)
                return NotFound("requested parent object does not exist");

            Game postGame = new();

            mapper.Map(game, postGame);
            postGame.Tournament = parent;

            TryValidateModel(postGame);
            if (!ModelState.IsValid)
                BadRequest();

            iUoW.GameRepository.Add(postGame);
            await iUoW.CompleteAsync();

            //Todo: fix id fetch?
            return CreatedAtAction("GetGame", new { id = postGame.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await iUoW.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound("no such object exists");
            }

            iUoW.GameRepository.Remove(game);
            await iUoW.CompleteAsync();

            return NoContent();
        }
    }
}
