using Service.Contracts.RequestObjects.Interfaces.Types;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.Interfaces.Requests
{
    public interface IRequestWithValidation<T1, T2> : IRequest<T1> where T2 : IDataValidator<Func<object, bool>>
    {
        bool InvokeValidate(object o);
    }
}