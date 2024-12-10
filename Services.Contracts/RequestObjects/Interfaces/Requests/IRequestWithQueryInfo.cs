using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.Interfaces.Requests
{
    public interface IRequestWithQueryInfo<T1, T2> : IRequest<T1> where T2 : IQueryInfo
    {
        T2 GetQueryInfo();
    }
}