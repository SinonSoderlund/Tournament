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
    public class RequestWithValidation<T1, T2> : Request<T1>, IRequestWithValidation<T1, T2> where T2 : IDataValidator<Func<object, bool>>
    {

        private T2 validator { get; set; }



        public RequestWithValidation(IAPIErrorSystem errorSystem, T2 validate, T1 Data = default!) : base(errorSystem, Data)
        {
            validator = validate;
        }

        public bool InvokeValidate(object o)
        { return validator.Validate(o); }
    }
}
