using Service.Contracts.RequestObjects.ErrorSystem;
using Service.Contracts.RequestObjects.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.Concrete.Requests
{
    public class Request<T1> : IRequest<T1>
    {
        private IAPIErrorSystem ErrorConnection;

        public T1 Data { get; set; }

        public Request(IAPIErrorSystem errorConnection, T1 data = default!)
        {
            ErrorConnection = errorConnection;
            Data = data;
        }
        public void CallError(ErrorInstance error)
        {
            ErrorConnection.RegisterError(error);
        }
    }
}
