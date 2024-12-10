using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Enums;
using Service.Contracts.Services;
using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Concrete.Requests;
using Service.Contracts.RequestObjects.Concrete.Types;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Interfaces.Types;
using System.Xml.Linq;
using Tournament.Core.Entities;

namespace Tournament.API.Controllers
{
    using PatchRequest = RequestWithValidationAndQueryInfo<JsonPatchDocument<TournamentIdDto>, IDataValidator<Func<object, bool>>, QueryInfoTournament>;
    using UpdateRequest = RequestWithValidation<TournamentUpdateDto, IDataValidator<Func<object, bool>>>;
    using PostRequest = RequestWithValidation<TournamentCreateDto, IDataValidator<Func<object, bool>>>;

    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase, IAPIErrorSystem
    {
        private readonly IServiceManager Services;
        private ErrorInstance currentError;
        public TournamentDetailsController(IServiceManager iServices)
        {
            this.Services = iServices;
            currentError = null!;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentIdDto>>> GetTournamentDetails()
        {
            var request = await Services.TournamentService.GetAllAsync(new Request<IEnumerable<TournamentIdDto>>(this));
            return Ok(request.Data);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentIdDto>> GetTournamentDetails(int id, bool? includeGames)
        {
            var request = await Services.TournamentService.GetAsync(new RequestWithQueryInfo<TournamentIdDto, QueryInfoTournament>(this, new(id, includeGames)));
            var tour = request.Data;

            if (tour == default)
            {
                return NotFound("Element does not exist");
            }

            return Ok(tour);
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
            await Services.TournamentService.UpdateAsync(new UpdateRequest(this, new DataValidator(TryValidateModel), tournamentDetails));
            if (currentError != null)
            {
                if (currentError.ErrorCode == EErrorCodes.BadRequest)
                {
                    TryValidateModel(tournamentDetails);
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                }
                return GenericErrors();
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentIdDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("Patch document cannot be null or empty");
            await Services.TournamentService.PatchAsync(new PatchRequest(this, new DataValidator(TryValidateModel), new QueryInfoTournament(id, null), patchDocument));
            if (currentError != null)
            {
                if (currentError.ErrorCode == EErrorCodes.BadRequest || currentError.ErrorCode == EErrorCodes.UnprocessableEntity)
                {
                    TryValidateModel(patchDocument);
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                }
                return GenericErrors();
            }

            return NoContent();
        }


        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournamentDetails(TournamentCreateDto tournamentDetails)
        {
            await Services.TournamentService.CreateAsync(new PostRequest(this, new DataValidator(TryValidateModel), tournamentDetails));
            if (currentError != null)
            {
                if (currentError.ErrorCode == EErrorCodes.BadRequest)
                {
                    TryValidateModel(tournamentDetails);
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                }
                return GenericErrors();
            }
            //Todo: fix id fetch?
            return CreatedAtAction("GetGame", -1, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            await Services.TournamentService.DeleteAsync(new Request<TournamentIdDto>(this, new TournamentIdDto { Id = id }));
            if (currentError != null)
            {
                return GenericErrors();
            }
            return NoContent();
        }

        public void RegisterError(ErrorInstance Error)
        {
            currentError = Error;
        }

        public ActionResult GenericErrors()
        {
            switch (currentError.ErrorCode)
            {
                case EErrorCodes.BadRequest:
                    return BadRequest(currentError.ErrorMessage);
                case EErrorCodes.NotFound:
                    return NotFound(currentError.ErrorMessage);
                case EErrorCodes.ISR500:
                    return StatusCode(500);
                case EErrorCodes.UnprocessableEntity:
                    return UnprocessableEntity(currentError.ErrorMessage);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
