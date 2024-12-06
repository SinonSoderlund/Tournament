using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Enums;
using Service.Contracts.Services;
using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.GameRequests;

namespace Tournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase, IAPIErrorSystem
    {
        private readonly IServiceManager Services;
        private ErrorInstance currentError;

        public GamesController(IServiceManager iService)
        {
            this.Services = iService;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameIdDto>>> GetGame()
        {
            IEnumerableGameIdDtoRequest request = new(this);
            request = await Services.GameService.GetAllAsync(request);
            return Ok(request.GameIdDtos);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameIdDto>> GetGame(int id, string? byName)
        {
            GameIdDtoRequest request = new(this, id, byName);
            request = await Services.GameService.GetAsync(request);
            var game = request.GameIdDto;

            if (game == default)
            {
                return NotFound("Element does not exist");
            }

            return Ok(game);
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
            await Services.GameService.UpdateAsync(new GameUpdateRequest(this, TryValidateModel, game));
            if (currentError != null)
            {
                if (currentError.ErrorCode == EErrorCodes.BadRequest)
                {
                    TryValidateModel(game);
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                }
                return GenericErrors();
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, JsonPatchDocument<GameIdDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("Patch document cannot be null or empty");
            await Services.GameService.PatchAsync(new GamePatchRequest(this, TryValidateModel, patchDocument, id));
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


        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameCreateDto game)
        {
            await Services.GameService.CreateAsync(new GameCreateRequest(this, TryValidateModel, game));
            if (currentError != null)
            {
                if (currentError.ErrorCode == EErrorCodes.BadRequest)
                {
                    TryValidateModel(game);
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                }
                return GenericErrors();
            }
            //Todo: fix id fetch?
            return CreatedAtAction("GetGame", -1, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await Services.GameService.DeleteAsync(new GameDeleteRequest(this, id));
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
