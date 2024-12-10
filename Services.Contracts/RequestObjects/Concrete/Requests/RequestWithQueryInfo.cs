using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Interfaces.Types;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.Concrete.Requests
{
    public class RequestWithQueryInfo<T1, T2> : Request<T1>, IRequestWithQueryInfo<T1, T2> where T2 : IQueryInfo
    {
        private T2 QueryInfo { get; set; }

        public RequestWithQueryInfo(IAPIErrorSystem errorSystem, T2 queryInfo, T1 data = default!) : base(errorSystem, data)
        {
            QueryInfo = queryInfo;
        }

        public T2 GetQueryInfo()
        {
            return QueryInfo;
        }
    }
}
