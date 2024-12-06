using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects
{
    public class RequestBase
    {
        private IAPIErrorSystem ErrorConnection;

        public RequestBase(IAPIErrorSystem errorConnection)
        {
            ErrorConnection = errorConnection;
        }

        public void CallError(ErrorInstance error)
        {
            ErrorConnection.RegisterError(error);
        }
    }
}
