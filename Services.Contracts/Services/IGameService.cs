using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    public interface IGameService //: IGenericService<IRequest<GameDto>, GameDto>
    {
        Task<IRequest<IEnumerable<GameIdDto>>> GetAllAsync(IRequest<IEnumerable<GameIdDto>> dtoRequest);
        Task<IRequestWithQueryInfo<GameIdDto, QueryInfoGame>> GetAsync(IRequestWithQueryInfo<GameIdDto, QueryInfoGame>  dtoRequest);
        Task CreateAsync(IRequestWithValidation<GameCreateDto, IDataValidator<Func<object,bool>>> createRequest);
        Task UpdateAsync(IRequestWithValidation<GameUpdateDto, IDataValidator<Func<object, bool>>> updateRequest);
        Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<GameIdDto>, IDataValidator<Func<object, bool>>, QueryInfoGame> patchRequest);
        Task DeleteAsync(IRequest<GameIdDto> deleteRequest);
    }
}
