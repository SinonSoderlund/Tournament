using AutoMapper;
using Service.Contracts.Services;
using Tournament.Core.Dto;
using Tournament.Core.Repositories;
using Service.Contracts.RequestObjects.GameRequests;
using Tournament.Core.Entities;
using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Enums;
using Microsoft.AspNetCore.JsonPatch;


namespace Tournament.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork UoW;
        private readonly IMapper mapper;

        public GameService(IUnitOfWork UoW, IMapper mapper)
        {
            this.UoW = UoW;
            this.mapper = mapper;
        }


        public async Task CreateAsync(GameCreateRequest createRequest)
        {
            TournamentDetails parent;

            var game = createRequest.GamecreateDto;

            parent = await UoW.TournamentRepository.GetAsync(game.TournamentId);

            if (parent == default)
            {
                createRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested parent object does not exist."));
                return;
            }

            Game postGame = new();

            mapper.Map(game, postGame);
            postGame.Tournament = parent;

            
            if (!createRequest.InvokeValidate(postGame))
            {
                createRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return;
            }

            UoW.GameRepository.Add(postGame);
            await UoW.CompleteAsync();
        }

        public async Task DeleteAsync(GameDeleteRequest deleteRequest)
        {
            var game = await UoW.GameRepository.GetAsync(deleteRequest.Id);
            if (game == default)
            {
                deleteRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            UoW.GameRepository.Remove(game);
            await UoW.CompleteAsync();
        }

        public async Task<IEnumerableGameIdDtoRequest> GetAllAsync(IEnumerableGameIdDtoRequest dtoRequest)
        {
            dtoRequest.GameIdDtos = mapper.Map<IEnumerable<GameIdDto>>(await UoW.GameRepository.GetAllAsync());
            return dtoRequest;
        }

        public async Task<GameIdDtoRequest> GetAsync(GameIdDtoRequest dtoRequest)
        {
            dtoRequest.GameIdDto = mapper.Map<GameIdDto>(await UoW.GameRepository.GetAsync(dtoRequest.Id, dtoRequest.Name));
            return dtoRequest;
        }

        public async Task PatchAsync(GamePatchRequest patchRequest)
        {
            if (patchRequest.PatchDocument is JsonPatchDocument<GameIdDto> patch)
            {
                Game game = await UoW.GameRepository.GetAsync(patchRequest.Id);

                if (game == default)
                {
                    patchRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                    return;
                }

                GameIdDto dto = mapper.Map<GameIdDto>(game);

                
                if (!patchRequest.InvokeValidate(dto))
                {
                    patchRequest.CallError(new ErrorInstance(EErrorCodes.UnprocessableEntity, "Something is wrong with the model."));
                    return;
                }

                mapper.Map(dto, game);
                if (!patchRequest.InvokeValidate(game))
                {
                    patchRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                    return;
                }

                UoW.GameRepository.Update(game);
                await UoW.CompleteAsync();
            }
            else
                throw new ArgumentException();
        }

        public async Task UpdateAsync(GameUpdateRequest updateRequest)
        {
            var game = updateRequest.GameUpdateDto;
            Game upd = await UoW.GameRepository.GetAsync(game.Id);

            if (upd == default)
            {
                updateRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            TournamentDetails parent;

            if (game.TournamentId != upd.TournamentId)
            {
                parent = await UoW.TournamentRepository.GetAsync(game.TournamentId);
                if (parent == default)
                {
                    updateRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested parent object does not exist."));
                    return;
                }
            }
            else
            {
                parent = upd.Tournament;
            }

            mapper.Map(game, upd);
            upd.Tournament = parent;


            if (updateRequest.InvokeValidate(upd))
            {
                updateRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return;
            }

            try
            {
                UoW.GameRepository.Update(upd);
                await UoW.CompleteAsync();
            }
            catch (Exception)
            {
                if (!await UoW.GameRepository.AnyAsync(game.Id))
                {
                    updateRequest.CallError(new ErrorInstance(EErrorCodes.ISR500, "Error Code 500, internal server error (probably)"));
                    return;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
