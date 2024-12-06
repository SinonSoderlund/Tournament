using Service.Contracts.RequestObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.ErrorSystem
{
    public class ErrorInstance
    {
        public EErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorInstance(EErrorCodes errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
