using Service.Contracts.RequestObjects.ErrorSystem;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.Interfaces.Requests
{
    public interface IRequest<T1>
    {
        T1 Data { get; set; }

        void CallError(ErrorInstance error);
    }
}