using Service.Contracts.RequestObjects.GameRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerableGameIdDtoRequest> GetAllAsync(IEnumerableGameIdDtoRequest dtoRequest);
        Task<GameIdDtoRequest> GetAsync(GameIdDtoRequest dtoRequest);
        Task CreateAsync(GameCreateRequest createRequest);
        Task UpdateAsync(GameUpdateRequest updateRequest);
        Task PatchAsync(GamePatchRequest patchRequest);
        Task DeleteAsync(GameDeleteRequest deleteRequest);
    }
}
