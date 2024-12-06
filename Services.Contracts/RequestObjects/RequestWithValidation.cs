using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects
{
    public class RequestWithValidation : RequestBase
    {

        private Func<object, bool> validate { get; set; }

        public RequestWithValidation(IAPIErrorSystem errorSystem, Func<object, bool> validate) : base(errorSystem)
        {
            this.validate = validate;
        }

        public bool InvokeValidate(object o)
        { return validate(o); }
    }
}
