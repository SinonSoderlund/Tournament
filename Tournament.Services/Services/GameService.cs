using AutoMapper;
using Service.Contracts.Services;
using Tournament.Core.Dto;
using Tournament.Core.Repositories;
using Tournament.Core.Entities;
using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Enums;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Concrete.Requests;


namespace Tournament.Services.Services
{
    using IDataValiation = IDataValidator<Func<object, bool>>;

    public class GameService : IGameService
    {
        private readonly IUnitOfWork UoW;
        private readonly IMapper mapper;

        public GameService(IUnitOfWork UoW, IMapper mapper)
        {
            this.UoW = UoW;
            this.mapper = mapper;
        }


        public async Task<IRequest<GameIdDto>> CreateAsync(IRequestWithValidation<GameCreateDto, IDataValiation> createRequest)
        {
            TournamentDetails parent;

            var game = createRequest.Data;

            parent = await UoW.TournamentRepository.GetAsync(game.TournamentId);

            if (parent == default)
            {
                createRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested parent object does not exist."));
                return null!;
            }

            Game postGame = new();

            mapper.Map(game, postGame);
            postGame.Tournament = parent;


            if (!createRequest.InvokeValidate(postGame))
            {
                createRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return null!;
            }

            UoW.GameRepository.Add(postGame);
            await UoW.CompleteAsync();
            GameIdDto idDto = mapper.Map<GameIdDto>(postGame);
            return new Request<GameIdDto>(idDto);
        }

        public async Task DeleteAsync(IRequest<GameIdDto> deleteRequest)
        {
            var game = await UoW.GameRepository.GetAsync(deleteRequest.Data.Id);
            if (game == default)
            {
                deleteRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            UoW.GameRepository.Remove(game);
            await UoW.CompleteAsync();
        }

        public async Task<IRequest<IEnumerable<GameIdDto>>> GetAllAsync(IRequest<IEnumerable<GameIdDto>> dtoRequest)
        {
            dtoRequest.Data = mapper.Map<IEnumerable<GameIdDto>>(await UoW.GameRepository.GetAllAsync());
            return dtoRequest;
        }

        public async Task<IRequestWithQueryInfo<GameIdDto, QueryInfoGame>> GetAsync(IRequestWithQueryInfo<GameIdDto, QueryInfoGame> dtoRequest)
        {
            dtoRequest.Data = mapper.Map<GameIdDto>(await UoW.GameRepository.GetAsync(dtoRequest.GetQueryInfo().Id, dtoRequest.GetQueryInfo().Name));
            return dtoRequest;
        }

        public async Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<GameIdDto>, IDataValiation, QueryInfoGame> patchRequest)
        {

            Game game = await UoW.GameRepository.GetAsync(patchRequest.GetQueryInfo().Id);

            if (game == default)
            {
                patchRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            GameIdDto dto = mapper.Map<GameIdDto>(game);
            patchRequest.Data.ApplyTo(dto);


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

        public async Task UpdateAsync(IRequestWithValidation<GameUpdateDto, IDataValiation> updateRequest)
        {
            var game = updateRequest.Data;
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
