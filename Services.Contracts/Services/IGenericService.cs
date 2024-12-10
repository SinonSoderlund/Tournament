using Service.Contracts.RequestObjects.Interfaces.Requests;
namespace Service.Contracts.Services
{
    public interface IGenericService <T1, T2> where T1 : IRequest<T2>
    {
        Task<IEnumerable<T1>> GetAllAsync(T1 dtoRequest);
        Task<T1> GetAsync(T1 dtoRequest);
        Task CreateAsync(T1 createRequest);
        Task UpdateAsync(T1 updateRequest);
        Task PatchAsync(T1 patchRequest);
        Task DeleteAsync(T1 deleteRequest);
    }
}
