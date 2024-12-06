﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Dto;
using AutoMapper;
using Bogus.DataSets;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IUnitOfWork iUoW;
        private readonly IMapper mapper;

        public TournamentDetailsController(IUnitOfWork iUoW, IMapper mapper)
        {
            this.iUoW = iUoW;
            this.mapper = mapper;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentIdDto>>> GetTournamentDetails()
        {
            return Ok(mapper.Map<IEnumerable<TournamentIdDto>>(await iUoW.TournamentRepository.GetAllAsync()));
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentIdDto>> GetTournamentDetails(int id, bool? includeGames)
        {
            var tournamentDetails = await iUoW.TournamentRepository.GetAsync(id, includeGames != null ? includeGames.Value : false);

            if (tournamentDetails == null)
            {
                return NotFound("Element does not exist");
            }

            return Ok(mapper.Map<TournamentIdDto>(tournamentDetails));
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto tournamentDetails)
        {
            if (id != tournamentDetails.Id)
            {
                return BadRequest("Target Url Id does not match inserted object");
            }


            TournamentDetails toAdd = await iUoW.TournamentRepository.GetAsync(id);

            if (toAdd == default)
                return NotFound("Requested object does not exist");

            mapper.Map(tournamentDetails, toAdd);

            TryValidateModel(toAdd);
            if (!ModelState.IsValid)
                BadRequest();


            try
            {
                iUoW.TournamentRepository.Update(toAdd);
                await iUoW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await iUoW.TournamentRepository.AnyAsync(id))
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
        public async Task<ActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentIdDto> patchDocument)
        {
            if(patchDocument == null)
                return BadRequest("Patch document cannot be null or empty");

            TournamentDetails tourDets = await iUoW.TournamentRepository.GetAsync(id);

            if(tourDets == default)
                return NotFound("Requested object does not exist.");

            TournamentIdDto dto = mapper.Map<TournamentIdDto>(tourDets);

            patchDocument.ApplyTo(dto, ModelState);
            if(!ModelState.IsValid) 
                UnprocessableEntity(ModelState);

            mapper.Map(dto, tourDets);
            if (!TryValidateModel(tourDets))
                BadRequest("Invalid patch attempt.");

            iUoW.TournamentRepository.Update(tourDets);
            await iUoW.CompleteAsync();
            
            return NoContent();
        }


        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournamentDetails(TournamentCreateDto tournamentDetails)
        {
            TournamentDetails postTour = new();

            mapper.Map(tournamentDetails, postTour);

            TryValidateModel(postTour);
            if (!ModelState.IsValid)
                BadRequest();


            iUoW.TournamentRepository.Add(postTour);
            await iUoW.CompleteAsync();

            return CreatedAtAction("GetTournamentDetails", new { id = postTour.Id }, tournamentDetails);
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
