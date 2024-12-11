using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.RequestObjects.Concrete.Types;
using Service.Contracts.RequestObjects.ConcreteType.Types;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.Services
{
    using IDataValiation = IDataValidator<Func<object, bool>>;
    public interface ITournamentService //: IGenericService<IRequest<TournamentDto>, TournamentDto>
    {
        Task<IRequest<IEnumerable<TournamentIdDto>>> GetAllAsync(IRequest<IEnumerable<TournamentIdDto>> dtoRequest);
        Task<IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament>> GetAsync(IRequestWithQueryInfo<TournamentIdDto, QueryInfoTournament> dtoRequest);
        Task<IRequest<TournamentIdDto>> CreateAsync(IRequestWithValidation<TournamentCreateDto, IDataValiation> createRequest);
        Task UpdateAsync(IRequestWithValidation<TournamentUpdateDto, IDataValiation> updateRequest);
        Task PatchAsync(IRequestWithValidationAndQueryInfo<JsonPatchDocument<TournamentIdDto>, IDataValiation, QueryInfoTournament> patchRequest);
        Task DeleteAsync(IRequest<TournamentIdDto> deleteRequest);
    }
}
