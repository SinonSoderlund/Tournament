using Service.Contracts.RequestObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.ErrorSystem
{
    public interface IAPIErrorSystem
    {
        public void RegisterError(ErrorInstance Error);
    }
}
