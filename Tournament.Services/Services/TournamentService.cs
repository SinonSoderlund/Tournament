using AutoMapper;
using Service.Contracts.RequestObjects.TournamentRequests;
using Service.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;
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

        public Task CreateAsync(TournamentCreateRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TournamentDeleteRequest deleteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerableTournamentIdDtoRequest> GetAllAsync(IEnumerableTournamentIdDtoRequest dtoRequest)
        {
            dtoRequest.TournamentIdDtos = mapper.Map<IEnumerable<TournamentIdDto>>(await UoW.TournamentRepository.GetAllAsync());
            return dtoRequest;
        }

        public async Task<TournamentIdDtoRequest> GetAsync(TournamentIdDtoRequest dtoRequest)
        {
            dtoRequest.TournamentIdDto = mapper.Map<TournamentIdDto>(await UoW.TournamentRepository.GetAsync(dtoRequest.Id, dtoRequest.includeGames != null ? dtoRequest.includeGames.Value : false));
            return dtoRequest;
        }

        public Task PatchAsync(TournamentPatchRequest patchRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TournamentUpdateRequest updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
