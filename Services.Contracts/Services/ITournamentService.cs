using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Concrete.Types;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    public interface ITournamentService //: IGenericService<IRequest<TournamentDto>, TournamentDto>
    {
        Task<IRequest<IEnumerable<TournamentIdDto>>> GetAllAsync(IRequest<IEnumerable<TournamentIdDto>> dtoRequest);
        Task<IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament>> GetAsync(IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament> dtoRequest);
        Task CreateAsync(IRequestWithValidation<TournamentCreateDto, IDataValidator<Func<object, bool>>> createRequest);
        Task UpdateAsync(IRequestWithValidation<TournamentUpdateDto, IDataValidator<Func<object, bool>>> updateRequest);
        Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<TournamentIdDto>, IDataValidator<Func<object, bool>>, QueryInfoTournament> patchRequest);
        Task DeleteAsync(IRequest<TournamentIdDto> deleteRequest);
    }
}
