using Service.Contracts.RequestObjects.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.Interfaces.Requests
{
    public interface IRequestWithValidationAndQueryInfo<T1, T2, T3> : IRequestWithValidation <T1, T2>, IRequestWithQueryInfo<T1, T3> where T2 : IDataValidator<Func<object, bool>> where T3 : IQueryInfo
    {
    }
}
