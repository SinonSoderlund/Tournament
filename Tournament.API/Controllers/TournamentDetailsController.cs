using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IUnitOfWork iUoW;

        public TournamentDetailsController(IUnitOfWork iUoW)
        {
            this.iUoW = iUoW;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails()
        {
            return Ok(await iUoW.TournamentRepository.GetAllAsync());
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
        {
            var tournamentDetails = await iUoW.TournamentRepository.GetAsync(id);

            if (tournamentDetails == null)
            {
                return NotFound();
            }

            return Ok(tournamentDetails);
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails tournamentDetails)
        {
            if (id != tournamentDetails.Id)
            {
                return BadRequest();
            }

            

            try
            {
                iUoW.TournamentRepository.Update(tournamentDetails);
                await iUoW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await iUoW.TournamentRepository.AnyAsync(id))
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

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetails tournamentDetails)
        {
            iUoW.TournamentRepository.Add(tournamentDetails);
            await iUoW.CompleteAsync();

            return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            var tournamentDetails = await iUoW.TournamentRepository.GetAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            iUoW.TournamentRepository.Remove(tournamentDetails);
            await iUoW.CompleteAsync();

            return NoContent();
        }
    }
}
