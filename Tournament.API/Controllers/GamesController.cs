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

namespace Tournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork iUoW;

        public GamesController(IUnitOfWork iUoW)
        {
            this.iUoW = iUoW;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            return Ok(await iUoW.GameRepository.GetAllAsync());
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await iUoW.GameRepository.GetAsync(id);

            if (game == default)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
           

            try
            {
                iUoW.GameRepository.Update(game);
                await iUoW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await iUoW.GameRepository.AnyAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            iUoW.GameRepository.Add(game);
            await iUoW.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await iUoW.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            iUoW.GameRepository.Remove(game);
            await iUoW.CompleteAsync();

            return NoContent();
        }
    }
}
