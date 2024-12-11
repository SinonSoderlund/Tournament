using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    using IDataValiation = IDataValidator<Func<object, bool>>;

    public interface IGameService //: IGenericService<IRequest<GameDto>, GameDto>
    {
        Task<IRequest<IEnumerable<GameIdDto>>> GetAllAsync(IRequest<IEnumerable<GameIdDto>> dtoRequest);
        Task<IRequestWithQueryInfo<GameIdDto, QueryInfoGame>> GetAsync(IRequestWithQueryInfo<GameIdDto, QueryInfoGame>  dtoRequest);
        Task<IRequest<GameIdDto>> CreateAsync(IRequestWithValidation<GameCreateDto, IDataValiation> createRequest);
        Task UpdateAsync(IRequestWithValidation<GameUpdateDto, IDataValiation> updateRequest);
        Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<GameIdDto>, IDataValiation, QueryInfoGame> patchRequest);
        Task DeleteAsync(IRequest<GameIdDto> deleteRequest);
    }
}
