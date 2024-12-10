using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Concrete.Types;
using Service.Contracts.RequestObjects.Enums;
using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Service.Contracts.Services;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Services.Services
{
    public class TournamentService : ITournamentService
    {

        private readonly IUnitOfWork UoW;
        private readonly IMapper mapper;

        public TournamentService(IUnitOfWork UoW, IMapper mapper)
        {
            this.UoW = UoW;
            this.mapper = mapper;
        }

        public async Task CreateAsync(IRequestWithValidation<TournamentCreateDto, IDataValidator<Func<object, bool>>> createRequest)
        {
            var tour = createRequest.Data;

            TournamentDetails deets = new TournamentDetails();

            mapper.Map(tour, deets);

            if (!createRequest.InvokeValidate(deets))
            {
                createRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return;
            }

            UoW.TournamentRepository.Add(deets);
            await UoW.CompleteAsync();
        }

        public async Task DeleteAsync(IRequest<TournamentIdDto> deleteRequest)
        {
            var tour = await UoW.TournamentRepository.GetAsync(deleteRequest.Data.Id);
            if (tour == default)
            {
                deleteRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            UoW.TournamentRepository.Remove(tour);
            await UoW.CompleteAsync();
        }

        public async Task<IRequest<IEnumerable<TournamentIdDto>>> GetAllAsync(IRequest<IEnumerable<TournamentIdDto>> dtoRequest)
        {
            dtoRequest.Data = mapper.Map<IEnumerable<TournamentIdDto>>(await UoW.TournamentRepository.GetAllAsync());
            return dtoRequest;
        }

        public async Task<IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament>> GetAsync(IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament> dtoRequest)
        {
            bool ig = dtoRequest.GetQueryInfo().includeGames != null ? dtoRequest.GetQueryInfo().includeGames!.Value : false;
            dtoRequest.Data = mapper.Map<TournamentIdDto>(await UoW.TournamentRepository.GetAsync(dtoRequest.GetQueryInfo().Id, ig));
            return dtoRequest;
        }

        public async Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<TournamentIdDto>, IDataValidator<Func<object, bool>>, QueryInfoTournament> patchRequest)
        {

            TournamentDetails tour = await UoW.TournamentRepository.GetAsync(patchRequest.GetQueryInfo().Id);

            if (tour == default)
            {
                patchRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            TournamentIdDto dto = mapper.Map<TournamentIdDto>(tour);


            if (!patchRequest.InvokeValidate(dto))
            {
                patchRequest.CallError(new ErrorInstance(EErrorCodes.UnprocessableEntity, "Something is wrong with the model."));
                return;
            }

            mapper.Map(dto, tour);
            if (!patchRequest.InvokeValidate(tour))
            {
                patchRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return;
            }

            UoW.TournamentRepository.Update(tour);
            await UoW.CompleteAsync();
        }

        public async Task UpdateAsync(IRequestWithValidation<TournamentUpdateDto, IDataValidator<Func<object, bool>>> updateRequest)
        {
            var tour = updateRequest.Data;
            TournamentDetails upd = await UoW.TournamentRepository.GetAsync(tour.Id);

            if (upd == default)
            {
                updateRequest.CallError(new ErrorInstance(EErrorCodes.NotFound, "Requested object does not exist."));
                return;
            }

            mapper.Map(tour, upd);


            if (updateRequest.InvokeValidate(upd))
            {
                updateRequest.CallError(new ErrorInstance(EErrorCodes.BadRequest, "Something is wrong with the model."));
                return;
            }

            try
            {
                UoW.TournamentRepository.Update(upd);
                await UoW.CompleteAsync();
            }
            catch (Exception)
            {
                if (!await UoW.GameRepository.AnyAsync(tour.Id))
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
