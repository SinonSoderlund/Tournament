using Service.Contracts.RequestObjects.TournamentRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    public interface ITournamentService
    {
        Task<IEnumerableTournamentIdDtoRequest> GetAllAsync(IEnumerableTournamentIdDtoRequest dtoRequest);
        Task<TournamentIdDtoRequest> GetAsync(TournamentIdDtoRequest dtoRequest);
        Task CreateAsync(TournamentCreateRequest createRequest);
        Task UpdateAsync(TournamentUpdateRequest updateRequest);
        Task PatchAsync(TournamentPatchRequest patchRequest);
        Task DeleteAsync(TournamentDeleteRequest deleteRequest);
    }
}
