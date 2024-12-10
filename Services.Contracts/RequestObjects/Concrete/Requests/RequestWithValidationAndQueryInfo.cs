using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using Service.Contracts.RequestObjects.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.Concrete.Requests
{
    public class RequestWithValidationAndQueryInfo<T1, T2, T3> : RequestWithValidation<T1, T2>, IRequestWithValidationAndQueryInfo<T1, T2, T3> where T2 : IDataValidator<Func<object, bool>> where T3 : IQueryInfo
    {
        private T3 queryInfo { get; set; }

        public RequestWithValidationAndQueryInfo(IAPIErrorSystem errorSystem, T2 validation, T3 queryInfo, T1 data = default!) : base(errorSystem, validation, data)
        {
            this.queryInfo = queryInfo;
        }

        public T3 GetQueryInfo()
        {
            return queryInfo;
        }
    }
}
