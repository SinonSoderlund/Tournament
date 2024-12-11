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

namespace Tournament.Presentation.Controllers
{
    using PatchRequest = RequestWithValidationAndQueryInfo<JsonPatchDocument<GameIdDto>, IDataValidator<Func<object, bool>>, QueryInfoGame>;
    using UpdateRequest = RequestWithValidation<GameUpdateDto, IDataValidator<Func<object, bool>>>;
    using PostRequest = RequestWithValidation<GameCreateDto, IDataValidator<Func<object, bool>>>;

    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase, IAPIErrorSystem
    {
        private readonly IServiceManager Services;
        private ErrorInstance currentError;

        public GamesController(IServiceManager iService)
        {
            Services = iService;
            currentError = null!;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameIdDto>>> GetGame()
        {
            var request = await Services.GameService.GetAllAsync(new Request<IEnumerable<GameIdDto>>(this));
            return Ok(request.Data);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameIdDto>> GetGame(int id, string? byName)
        {
            var request = await Services.GameService.GetAsync(new RequestWithQueryInfo<GameIdDto, QueryInfoGame>(this, new(id, byName)));
            var game = request.Data;

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
            await Services.GameService.UpdateAsync(new UpdateRequest(this, new DataValidator(TryValidateModel), game));
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
            await Services.GameService.PatchAsync(new PatchRequest(this, new DataValidator(TryValidateModel), new QueryInfoGame(id, null), patchDocument));
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
            var outp = await Services.GameService.CreateAsync(new PostRequest(this, new DataValidator(TryValidateModel), game));
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
            return CreatedAtAction("GetGame", new { id = outp.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await Services.GameService.DeleteAsync(new Request<GameIdDto>(this, new GameIdDto { Id = id }));
            if (currentError != null)
            {
                return GenericErrors();
            }
            return NoContent();

        }

        [NonAction]
        public void RegisterError(ErrorInstance Error)
        {
            currentError = Error;
        }

        private ActionResult GenericErrors()
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
